using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Todos.Source.Persistence.Migrations
{
    public static class Migrator
    {
        private static readonly IDictionary<int, MigrationBase> _migrations = new Dictionary<int, MigrationBase>
        {
            {1, new MakeScheduleMandatory() }
        };

        public static string Migrate(string jsonString)
        {
            var json = JObject.Parse(jsonString);
            var fileVersion = json["Version"]?.ToObject<int>();
            Debug.Assert(fileVersion != null, nameof(fileVersion) + " != null");

            if (fileVersion.Value == TodoJson.CURRENT_VERSION)
                return jsonString;
            
            for (var version = fileVersion.Value; version < TodoJson.CURRENT_VERSION; version++)
                _migrations[version].Migrate(json);

            json["Version"] = TodoJson.CURRENT_VERSION;
            return json.ToString();
        }
    }
}