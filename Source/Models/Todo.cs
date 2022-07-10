using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Todo
    {
        public const int CURRENT_VERSION = 1;
        
        public bool IsNew { get; private set; }
        public bool IsDeleted { get; private set; }
        
        [JsonProperty] public int Version { get; private set; }
        [JsonProperty] public DateTime CreatedAt { get; private set; }
        [JsonProperty] public string Description { get; set; }
        [JsonProperty] public List<DateTime> Executions { get; private set; }
        [JsonProperty] public TodoSchedule? Schedule { get; set; }
        [JsonProperty] public string ClipboardContent { get; set; }

        public Todo() : this(CURRENT_VERSION, DateTime.Now, new List<DateTime>())
        {
            IsNew = true;
            Schedule = new TodoSchedule
            {
                Type = TodoScheduleType.DailyServer,
                Duration = TimeSpan.FromDays(1)
            };
        }
        
        [JsonConstructor]
        private Todo(int version, DateTime createdAt, List<DateTime> executions)
        {
            Version = version;
            CreatedAt = createdAt;
            Executions = executions;
        }

        public DateTime? LastExecution => Executions.Count > 0 ? Executions.Max().WithoutSeconds() : (DateTime?)null;
        
        public bool Done
        {
            get
            {
                var lastExecution = LastExecution;
                if (!Schedule.HasValue)
                    return lastExecution.HasValue;

                switch (Schedule.Value.Type)
                {
                    case TodoScheduleType.DailyServer:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastDailyReset;
                    case TodoScheduleType.WeeklyServer:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastWeeklyReset;
                    case TodoScheduleType.LocalTime:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastLocalReset(Schedule.Value);
                    case TodoScheduleType.Duration:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastDurationReset(Schedule.Value);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (value) Executions.Add(DateTime.Now.WithoutSeconds());
                else
                {
                    if (Executions.Count > 0)
                        Executions.RemoveAt(Executions.IndexOf(Executions.Max()));
                }
            }
        }
        
        public void Save()
        {
            Data.Save(this);
        }

        public void Delete()
        {
            IsDeleted = true;
            Data.Delete(this);
        }
    }
}