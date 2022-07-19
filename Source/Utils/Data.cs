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
        private static readonly object _that = new object();

        private static IVariable<List<TodoModel>> _allTodos; 
        public static IProperty<IReadOnlyList<TodoModel>> AllTodos => _allTodos;
        
        public static IProperty<IReadOnlyList<TodoModel>> VisibleTodos;

        public static async Task Initialize(DirectoriesManager manager)
        {
            var storedTodos = await new Persistence.Persistence(manager).LoadAll();
            var sortedTodos = storedTodos.OrderBy(t => t.OrderIndex.Value).ToList();

            _allTodos = Variables.Transient(sortedTodos);
            VisibleTodos = AllTodos.Select(all => all.Where(t => t.IsVisible.Value).ToList());

            foreach (var todo in sortedTodos)
                SetupTodo(todo);
        }

        private static void SetupTodo(TodoModel todo)
        {
            todo.IsDeleted.Subscribe(_that, _ => OnTodoDeleted(todo), false);
            todo.OrderIndex.Subscribe(_that, OnOrderChanged);
            todo.IsVisible.Subscribe(_that, OnVisibilityChanged);
        }

        private static void TearDownTodo(TodoModel todo)
        {
            todo.IsDeleted.Unsubscribe(_that);
            todo.OrderIndex.Unsubscribe(_that);
            todo.IsVisible.Unsubscribe(_that);
            todo.Dispose();
        }

        private static void OnVisibilityChanged(bool isVisible)
        {
            _allTodos.Value = AllTodos.Value.OrderBy(t => t.OrderIndex.Value).ToList();
        }

        private static void OnOrderChanged(long newValue)
        {
            _allTodos.Value = AllTodos.Value.OrderBy(t => t.OrderIndex.Value).ToList();
        }

        public static void AddNewTodo()
        {
            var todo = new TodoModel(new TodoJson(), true);
            SetupTodo(todo);
            _allTodos.Value = AllTodos.Value.Append(todo).ToList();
        }
        
        public static void MoveUp(TodoModel todo)
        {
            var todos = new List<TodoModel>(AllTodos.Value);
            var topIndex = todos.FindLastIndex(t => t.IsVisible.Value && t.OrderIndex.Value < todo.OrderIndex.Value);
            var bottomIndex = todos.IndexOf(todo);
            var newOrderIndexes = new Dictionary<TodoModel, long>();

            for (var i = topIndex; i < bottomIndex; i++)
                newOrderIndexes[todos[i]] = todos[i + 1].OrderIndex.Value;
            newOrderIndexes[todo] = todos[topIndex].OrderIndex.Value;

            foreach (var update in newOrderIndexes)
                update.Key.OrderIndex.Value = update.Value;
        }
        
        public static void MoveDown(TodoModel todo)
        {
            var todos = new List<TodoModel>(AllTodos.Value);
            var topIndex = todos.IndexOf(todo);
            var bottomIndex = todos.FindIndex(t => t.IsVisible.Value && t.OrderIndex.Value > todo.OrderIndex.Value);
            var newOrderIndexes = new Dictionary<TodoModel, long>();

            for (var i = topIndex + 1; i <= bottomIndex; i++)
                newOrderIndexes[todos[i]] = todos[i - 1].OrderIndex.Value;
            newOrderIndexes[todo] = todos[bottomIndex].OrderIndex.Value;

            foreach (var update in newOrderIndexes)
                update.Key.OrderIndex.Value = update.Value;
        }

        private static void OnTodoDeleted(TodoModel todo)
        {
            TearDownTodo(todo);
            _allTodos.Value = AllTodos.Value.Where(t => t != todo).ToList();
        }

        public static void Dispose()
        {
            foreach (var todo in AllTodos.Value)
                TearDownTodo(todo);
            AllTodos.Dispose();
            VisibleTodos.Dispose();
        }
    }
}