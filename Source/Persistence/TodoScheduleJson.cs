using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Todos.Source.Persistence
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TodoScheduleJson
    {
        public TodoJson Parent;
        
        [JsonProperty] public TodoScheduleType Type;
        
        [JsonProperty] public TimeSpan LocalTime;
        [JsonProperty] public TimeSpan Duration;

        [JsonProperty] public List<DateTimeOffset> Executions;

        public TodoScheduleJson()
        {
            Type = TodoScheduleType.DailyServer;
            Executions = new List<DateTimeOffset>();
            Duration = TimeSpan.FromDays(1);
        }

        public void Persist()
        {
            SaveScheduler.MarkAsChanged(Parent);
        }
    }
}