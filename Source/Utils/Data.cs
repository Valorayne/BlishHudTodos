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
        
        public static IReadOnlyList<TodoModel> Todos => _todos.Values.OrderBy(todo => todo.OrderIndex.Value).ToList();

        public static event EventHandler<TodoModel> TodoAdded;
        public static event EventHandler<bool> AnyDoneChanged; 
        public static event EventHandler<TodoModel> TodoDeleted;
        public static event EventHandler TodoOrderChanged;

        public static async Task Initialize(DirectoriesManager manager)
        {
            _todos = new Dictionary<long, TodoModel>();
            foreach (var todo in await new Persistence.Persistence(manager).LoadAll())
                AddToDictionary(todo);
        }

        public static void AddNewTodo()
        {
            var todo = new TodoModel(new TodoJson(), true);
            AddToDictionary(todo);
            TodoAdded?.Invoke(todo, todo);
        }
        
        public static void MoveUp(TodoModel todo)
        {
            var todos = new List<TodoModel>(Todos);
            var topIndex = todos.FindLastIndex(t => t.IsVisible.Value && t.OrderIndex.Value < todo.OrderIndex.Value);
            var bottomIndex = todos.IndexOf(todo);
            var newOrderIndexes = new Dictionary<TodoModel, long>();

            for (var i = topIndex; i < bottomIndex; i++)
                newOrderIndexes[todos[i]] = todos[i + 1].OrderIndex.Value;
            newOrderIndexes[todo] = todos[topIndex].OrderIndex.Value;

            foreach (var update in newOrderIndexes)
                update.Key.OrderIndex.Value = update.Value;

            TodoOrderChanged?.Invoke(todo, EventArgs.Empty);
        }
        
        public static void MoveDown(TodoModel todo)
        {
            var todos = new List<TodoModel>(Todos);
            var topIndex = todos.IndexOf(todo);
            var bottomIndex = todos.FindIndex(t => t.IsVisible.Value && t.OrderIndex.Value > todo.OrderIndex.Value);
            var newOrderIndexes = new Dictionary<TodoModel, long>();

            for (var i = topIndex + 1; i <= bottomIndex; i++)
                newOrderIndexes[todos[i]] = todos[i - 1].OrderIndex.Value;
            newOrderIndexes[todo] = todos[bottomIndex].OrderIndex.Value;

            foreach (var update in newOrderIndexes)
                update.Key.OrderIndex.Value = update.Value;

            TodoOrderChanged?.Invoke(todo, EventArgs.Empty);
        }

        private static void AddToDictionary(TodoModel todo)
        {
            _todos.Add(todo.CreatedAt.Ticks, todo);
            todo.IsDeleted.PropertyChanged += OnTodoDeleted;
            todo.DoneChanged += OnDoneChanged;
        }

        private static void OnDoneChanged(bool newValue)
        {
            AnyDoneChanged?.Invoke(newValue, newValue);
        }

        private static void OnTodoDeleted(object owner, bool newValue)
        {
            _todos.Remove(((TodoModel) owner).CreatedAt.Ticks);
            TodoDeleted?.Invoke(owner, (TodoModel) owner);
        }

        public static void Dispose()
        {
            foreach (var todo in _todos.Values)
            {
                todo.IsDeleted.PropertyChanged -= OnTodoDeleted;
                todo.DoneChanged -= OnDoneChanged;
                todo.Dispose();
            }
            _todos?.Clear();
            _todos = null;
        }
    }
}