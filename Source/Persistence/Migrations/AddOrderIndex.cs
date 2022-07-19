using System;
using Newtonsoft.Json.Linq;

namespace Todos.Source.Persistence.Migrations
{
    public class AddOrderIndex : MigrationBase
    {
        public override void Migrate(JObject json)
        {
            var createdAt = json["CreatedAt"].ToObject<DateTime>();
            if (!json.ContainsKey("OrderIndex"))
                json["OrderIndex"] = createdAt.Ticks;
        }
    }
}