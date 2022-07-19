using System.Linq;
using Newtonsoft.Json.Linq;

namespace Todos.Source.Persistence.Migrations
{
    public class MoveExecutionsIntoSchedule : MigrationBase
    {
        public override void Migrate(JObject json)
        {
            var executions = json["Executions"].ToArray();
            json.Remove("Executions");
            json["Schedule"]["Executions"] = JArray.FromObject(executions);
        }
    }
}