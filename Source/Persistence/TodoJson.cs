using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Persistence
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TodoJson
    {
        public const int CURRENT_VERSION = 1;
        
        [JsonProperty] public int Version { get; private set; }
        [JsonProperty] public DateTime CreatedAt { get; private set; }
        [JsonProperty] public List<DateTime> Executions { get; private set; }
        
        public bool IsDeleted { get; set; }
        
        [JsonProperty] public string Description { get; set; }
        [JsonProperty] public TodoSchedule? Schedule { get; set; }
        [JsonProperty] public string ClipboardContent { get; set; }

        [JsonConstructor]
        private TodoJson(int version, DateTime createdAt, List<DateTime> executions)
        {
            Version = version;
            CreatedAt = createdAt;
            Executions = executions;
        }

        public TodoJson() : this(CURRENT_VERSION, DateTime.Now, new List<DateTime>())
        {
            Schedule = new TodoSchedule
            {
                Type = TodoScheduleType.DailyServer,
                Duration = TimeSpan.FromDays(1),
                LocalTime = TimeSpan.Zero
            };
        }

        public void Persist()
        {
            SaveScheduler.MarkAsChanged(this);
        }
    }
}