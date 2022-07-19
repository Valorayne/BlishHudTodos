using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Todos.Source.Models;

namespace Todos.Source.Persistence
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TodoJson
    {
        public const int CURRENT_VERSION = 3;
        
        [JsonProperty] public int Version { get; private set; }
        [JsonProperty] public DateTime CreatedAt { get; private set; }
        [JsonProperty] public List<DateTime> Executions { get; private set; }
        [JsonProperty] public TodoScheduleJson Schedule { get; private set; }
        
        public bool IsDeleted { get; set; }
        
        [JsonProperty] public long OrderIndex { get; set; }
        [JsonProperty] public string Description { get; set; }
        [JsonProperty] public string ClipboardContent { get; set; }

        [JsonConstructor]
        private TodoJson(int version, DateTime createdAt, List<DateTime> executions, TodoScheduleJson schedule)
        {
            Version = version;
            CreatedAt = createdAt;
            Executions = executions;
            Schedule = schedule;
        }

        public TodoJson() : this(CURRENT_VERSION, DateTime.Now, new List<DateTime>(),
            new TodoScheduleJson { Duration = TimeSpan.FromDays(1) })
        {
            OrderIndex = CreatedAt.Ticks;
        }

        public void Persist()
        {
            SaveScheduler.MarkAsChanged(this);
        }
    }
}