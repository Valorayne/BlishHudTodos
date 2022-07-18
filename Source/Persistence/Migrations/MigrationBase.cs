using Newtonsoft.Json.Linq;

namespace Todos.Source.Persistence.Migrations
{
    public abstract class MigrationBase
    {
        public abstract void Migrate(JObject json);
        
        protected static bool IsNotSet(JToken token) => token.Type == JTokenType.Null ||
                                                      token.Type == JTokenType.Undefined ||
                                                      token.Type == JTokenType.None;
    }
}