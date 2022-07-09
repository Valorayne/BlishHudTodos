using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blish_HUD.Modules.Managers;
using Todos.Source.Models;

namespace Todos.Source.Utils
{
    public static class Data
    {
        private static Dictionary<long, Todo> _todos;
        
        public static IReadOnlyList<Todo> Todos => _todos.Values.ToList();

        public static event EventHandler<Todo> TodoAdded;
        public static event EventHandler<Todo> TodoModified;
        public static event EventHandler<Todo> TodoDeleted;

        public static Task Initialize(DirectoriesManager manager)
        {
            _todos = new Dictionary<long, Todo>();
            foreach (var todo in new Persistence(manager).LoadAll())
                _todos.Add(todo.CreatedAt.Ticks, todo);
            return Task.CompletedTask;
        }

        public static void Save(Todo todo)
        {
            TodoModified?.Invoke(todo, todo);
        }

        public static void Add(Todo todo)
        {
            _todos.Add(todo.CreatedAt.Ticks, todo);
            TodoAdded?.Invoke(todo, todo);
        }
        
        public static void Delete(Todo todo)
        {
            _todos.Remove(todo.CreatedAt.Ticks);
            TodoDeleted?.Invoke(todo, todo);
        }

        public static void Dispose()
        {
            _todos?.Clear();
            _todos = null;
        }
    }
}