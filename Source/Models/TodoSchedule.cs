using Newtonsoft.Json;

namespace TodoList.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public struct TodoSchedule
    {
        [JsonProperty] public TodoScheduleType Type;
    }
}