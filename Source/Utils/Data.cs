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
        public static Variable<IReadOnlyList<TodoModel>> AllTodos;
        public static Variable<IReadOnlyList<TodoModel>> VisibleTodos;

        public static async Task Initialize(DirectoriesManager manager)
        {
            var storedTodos = await new Persistence.Persistence(manager).LoadAll();
            var sortedTodos = storedTodos.OrderBy(t => t.OrderIndex.Value).ToList();
            
            VisibleTodos = new Variable<IReadOnlyList<TodoModel>>(null, sortedTodos.Where(t => t.IsVisible.Value).ToList());
            AllTodos = new Variable<IReadOnlyList<TodoModel>>(null, sortedTodos, 
                v => VisibleTodos.Value = v.Where(t => t.IsVisible.Value).ToList());

            foreach (var todo in sortedTodos)
                SetupTodo(todo);
        }

        private static void SetupTodo(TodoModel todo)
        {
            todo.IsDeleted.PropertyChanged += OnTodoDeleted;
            todo.OrderIndex.Changed += OnOrderChanged;
            todo.IsVisible.Changed += OnVisibilityChanged;
        }

        private static void TearDownTodo(TodoModel todo)
        {
            todo.IsDeleted.PropertyChanged -= OnTodoDeleted;
            todo.OrderIndex.Changed -= OnOrderChanged;
            todo.IsVisible.Changed -= OnVisibilityChanged;
            todo.Dispose();
        }

        private static void OnVisibilityChanged(bool isVisible)
        {
            VisibleTodos.Value = AllTodos.Value.OrderBy(t => t.OrderIndex.Value).Where(t => t.IsVisible.Value).ToList();
        }

        private static void OnOrderChanged(long newValue)
        {
            AllTodos.Value = AllTodos.Value.OrderBy(t => t.OrderIndex.Value).ToList();
        }

        public static void AddNewTodo()
        {
            var todo = new TodoModel(new TodoJson(), true);
            SetupTodo(todo);
            AllTodos.Value = AllTodos.Value.Append(todo).ToList();
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

        private static void OnTodoDeleted(object owner, bool newValue)
        {
            var todo = (TodoModel)owner;
            TearDownTodo(todo);
            AllTodos.Value = AllTodos.Value.Where(t => t != todo).ToList();
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