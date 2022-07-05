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
        
        [JsonProperty]
        public DateTime? LastExecution { get; private set; }

        public bool Done
        {
            get => LastExecution.HasValue;
            set
            {
                if (value)
                    LastExecution = DateTime.Now;
                else
                    LastExecution = null;
            }
        }

        public static Todo CreateDraft()
        {
            return new Todo(DateTime.Now, null)
            {
                IsDraft = true,
                Text = "This is some test content"
            };
        }

        [JsonConstructor]
        private Todo(DateTime createdAt, DateTime? lastExecution)
        {
            CreatedAt = createdAt;
            LastExecution = lastExecution;
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