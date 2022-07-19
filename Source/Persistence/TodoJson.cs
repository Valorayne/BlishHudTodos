using System;
using Newtonsoft.Json;
using Todos.Source.Models;
using Todos.Source.Persistence.Migrations;

namespace Todos.Source.Persistence
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TodoJson
    {
        public bool IsDeleted;

        [JsonProperty] public int Version;
        [JsonProperty] public DateTime CreatedAt;
        [JsonProperty] public TodoScheduleJson Schedule;

        [JsonProperty] public long OrderIndex;
        [JsonProperty] public string Description;
        [JsonProperty] public string ClipboardContent;

        public TodoJson()
        {
            Version = Migrator.CURRENT_VERSION;
            CreatedAt = DateTime.Now;
            Schedule = new TodoScheduleJson { Parent = this };
            OrderIndex = CreatedAt.Ticks;
        }

        [JsonConstructor]
        public TodoJson(TodoScheduleJson schedule)
        {
            Schedule = schedule;
            Schedule.Parent = this;
        }

        public void Persist()
        {
            SaveScheduler.MarkAsChanged(this);
        }
    }
}