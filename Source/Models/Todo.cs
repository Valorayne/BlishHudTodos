using System;
using Newtonsoft.Json;

namespace TodoList.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Todo
    {
        public bool IsDraft { get; private set; }
        
        [JsonProperty]
        public string Text { get; set; }
        
        [JsonProperty]
        public DateTime CreatedAt { get; private set; }

        public static Todo CreateDraft()
        {
            return new Todo(DateTime.Now)
            {
                IsDraft = true,
                Text = "This is some test content"
            };
        }

        [JsonConstructor]
        private Todo(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
        
        public void Save()
        {
            if (IsDraft)
                Data.Add(this);
            else
                Data.Save(this);
            
            IsDraft = false;
        }

        public void Delete()
        {
            Data.Delete(this);
        }
    }
}