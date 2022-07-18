using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Todos.Source.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TodoSchedule
    {
        [DefaultValue(TodoScheduleType.DailyServer)] 
        [JsonProperty] public TodoScheduleType Type;
        
        [JsonProperty] public TimeSpan LocalTime;
        [JsonProperty] public TimeSpan Duration;
    }
}