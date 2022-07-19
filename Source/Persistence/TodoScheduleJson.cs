using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Todos.Source.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TodoScheduleJson
    {
        [JsonProperty] public TodoScheduleType Type;
        
        [JsonProperty] public TimeSpan LocalTime;
        [JsonProperty] public TimeSpan Duration;

        [JsonProperty] public List<DateTime> Executions;

        public TodoScheduleJson()
        {
            Type = TodoScheduleType.DailyServer;
            Executions = new List<DateTime>();
            Duration = TimeSpan.FromDays(1);
        }
    }
}