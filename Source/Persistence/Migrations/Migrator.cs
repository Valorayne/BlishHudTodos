using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Todos.Source.Persistence.Migrations
{
    public static class Migrator
    {
        // ORDER IS CRUCIAL HERE. Always append, never change order
        private static readonly List<MigrationBase> _migrations = new List<MigrationBase>
        {
            new MakeScheduleMandatory(),
            new AddOrderIndex(),
            new MoveExecutionsIntoSchedule()
        };

        public static int CURRENT_VERSION = _migrations.Count + 1;

        public static string Migrate(string jsonString)
        {
            var json = JObject.Parse(jsonString);
            var fileVersion = json["Version"]?.ToObject<int>();
            Debug.Assert(fileVersion != null, nameof(fileVersion) + " != null");

            if (fileVersion.Value > CURRENT_VERSION)
                return null;
            
            if (fileVersion.Value == CURRENT_VERSION)
                return jsonString;
            
            for (var version = fileVersion.Value; version < CURRENT_VERSION; version++)
                _migrations[version-1].Migrate(json);

            json["Version"] = CURRENT_VERSION;
            return json.ToString();
        }
    }
}