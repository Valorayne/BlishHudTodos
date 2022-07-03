using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TodoList.Models;

namespace TodoList
{
    public static class Data
    {
        private static Dictionary<long, Todo> _todos;
        public static IReadOnlyList<Todo> Todos => _todos.Values.ToList();

        public static event EventHandler<Todo> TodoAdded;
        public static event EventHandler<Todo> TodoModified;
        public static event EventHandler<Todo> TodoDeleted;
        
        public static void Initialize()
        {
            var dataString = Settings.Data.Value;
            _todos = JsonConvert.DeserializeObject<Dictionary<long, Todo>>(dataString);
        }

        public static void Save(Todo todo)
        {
            Settings.Data.Value = JsonConvert.SerializeObject(_todos);
            TodoModified?.Invoke(todo, todo);
        }

        public static void Add(Todo todo)
        {
            _todos.Add(todo.CreatedAt.Ticks, todo);
            Settings.Data.Value = JsonConvert.SerializeObject(_todos);
            TodoAdded?.Invoke(todo, todo);
        }

        public static void Delete(Todo todo)
        {
            _todos.Remove(todo.CreatedAt.Ticks);
            Settings.Data.Value = JsonConvert.SerializeObject(_todos);
            TodoDeleted?.Invoke(todo, todo);
        }

        public static void Dispose()
        {
            _todos?.Clear();
            _todos = null;
        }
    }
}