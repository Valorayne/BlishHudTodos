using Newtonsoft.Json.Linq;

namespace Todos.Source.Persistence.Migrations
{
    public class MakeScheduleMandatory : MigrationBase
    {
        public override void Migrate(JObject json)
        {
            var schedule = json["Schedule"];
            if (IsNotSet(schedule))
            {
                json["Schedule"] = JToken.FromObject(new
                {
                    Type = -1,
                    LocalTime = "00:00:00",
                    Duration = "1.00:00:00"
                });
            }
        }
    }
}