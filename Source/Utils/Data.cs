using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blish_HUD.Modules.Managers;
using Todos.Source.Models;
using Todos.Source.Persistence;

namespace Todos.Source.Utils
{
    public static class Data
    {
        private static Dictionary<long, TodoModel> _todos;
        
        public static IReadOnlyList<TodoModel> Todos => _todos.Values.OrderBy(todo => todo.CreatedAt).ToList();

        public static event EventHandler<TodoModel> TodoAdded;
        public static event EventHandler<bool> AnyDoneChanged; 
        public static event EventHandler<TodoModel> TodoDeleted;

        public static async Task Initialize(DirectoriesManager manager)
        {
            _todos = new Dictionary<long, TodoModel>();
            foreach (var todo in await new Persistence(manager).LoadAll())
                AddToDictionary(todo);
        }

        public static void AddNewTodo()
        {
            var todo = new TodoModel(new TodoJson(), true);
            AddToDictionary(todo);
            TodoAdded?.Invoke(todo, todo);
        }

        private static void AddToDictionary(TodoModel todo)
        {
            _todos.Add(todo.CreatedAt.Ticks, todo);
            todo.Deleted += OnTodoDeleted;
            todo.DoneChanged += OnDoneChanged;
        }

        private static void OnDoneChanged(bool newValue)
        {
            AnyDoneChanged?.Invoke(newValue, newValue);
        }

        private static void OnTodoDeleted(TodoModel todo)
        {
            _todos.Remove(todo.CreatedAt.Ticks);
            TodoDeleted?.Invoke(todo, todo);
        }

        public static void Dispose()
        {
            foreach (var todo in _todos.Values)
            {
                todo.Deleted -= OnTodoDeleted;
                todo.DoneChanged -= OnDoneChanged;
            }
            _todos?.Clear();
            _todos = null;
        }
    }
}