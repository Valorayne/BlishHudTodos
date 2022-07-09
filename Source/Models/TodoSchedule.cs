using Newtonsoft.Json;

namespace Todos.Source.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public struct TodoSchedule
    {
        [JsonProperty] public TodoScheduleType Type;
    }
}