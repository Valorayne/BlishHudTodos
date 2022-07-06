using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TodoList.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Todo
    {
        public static Todo CreateDraft()
        {
            return new Todo(DateTime.Now, new List<DateTime>()) { IsDraft = true };
        }

        public bool IsDraft { get; private set; }
        
        [JsonProperty] public string Text { get; set; }
        [JsonProperty] public DateTime CreatedAt { get; private set; }
        [JsonProperty] public List<DateTime> Executions { get; private set; }
        [JsonProperty] public TodoSchedule? Schedule { get; set; }

        [JsonConstructor]
        private Todo(DateTime createdAt, List<DateTime> executions)
        {
            CreatedAt = createdAt;
            Executions = executions;
        }

        public DateTime? LastExecution => Executions.Count > 0 ? Executions.Last() : (DateTime?)null;
        
        public bool Done
        {
            get
            {
                if (!Schedule.HasValue)
                    return LastExecution.HasValue;

                switch (Schedule.Value.Type)
                {
                    case TodoScheduleType.DailyServer:
                        return LastExecution.HasValue && LastExecution.Value < DateUtils.LastDailyReset;
                    case TodoScheduleType.WeeklyServer:
                        return LastExecution.HasValue && LastExecution.Value < DateUtils.LastWeeklyReset;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (value) Executions.Add(DateTime.Now);
                else Executions.RemoveAt(Executions.Count - 1);
            }
        }
        
        public void Save()
        {
            if (IsDraft)
                Data.Add(this);
            else
                Data.Save(this);
            
            IsDraft = false;
        }

        public void Delete()
        {
            Data.Delete(this);
        }
    }
}