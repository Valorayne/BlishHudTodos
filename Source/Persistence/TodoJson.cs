using System;
using Newtonsoft.Json;
using Todos.Source.Models;

namespace Todos.Source.Persistence
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TodoJson
    {
        public const int CURRENT_VERSION = 4;

        [JsonProperty] public int Version;
        [JsonProperty] public DateTime CreatedAt;
        [JsonProperty] public TodoScheduleJson Schedule;

        public bool IsDeleted;

        [JsonProperty] public long OrderIndex;
        [JsonProperty] public string Description;
        [JsonProperty] public string ClipboardContent;

        public TodoJson()
        {
            Version = CURRENT_VERSION;
            CreatedAt = DateTime.Now;
            Schedule = new TodoScheduleJson();
            OrderIndex = CreatedAt.Ticks;
        }

        public void Persist()
        {
            SaveScheduler.MarkAsChanged(this);
        }
    }
}