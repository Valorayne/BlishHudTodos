using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blish_HUD.Modules.Managers;
using Todos.Source.Persistence;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Models
{
    public class TodoListModel : ModelBase
    {
        private readonly IVariable<List<TodoModel>> _allTodos;
        private readonly SettingsModel _settings;

        public readonly IVariable<TodoModel> MovingTodo;
        public readonly IProperty<IReadOnlyList<TodoModel>> VisibleTodos;

        private TodoListModel(SettingsModel settings, List<TodoModel> sortedTodos)
        {
            _settings = settings;
            _allTodos = Add(Variables.Transient(sortedTodos));
            VisibleTodos = Add(AllTodos.Select(all => all.Where(t => t.IsVisible.Value).ToList()));
            MovingTodo = Add(Variables.Transient<TodoModel>(null));

            foreach (var todo in sortedTodos)
                SetupTodo(todo);

            MouseService.LeftButton.Subscribe(this, state =>
            {
                if (MovingTodo.IsSet() && state == MouseService.ButtonState.Released)
                    MovingTodo.Unset();
            });
        }

        public IProperty<IReadOnlyList<TodoModel>> AllTodos => _allTodos;

        public static async Task<TodoListModel> Initialize(SettingsModel settings, DirectoriesManager manager)
        {
            var storedTodos = await new Persistence.Persistence(manager).LoadAll();
            var todoModels = storedTodos.Select(json => new TodoModel(settings, json, false));
            return new TodoListModel(settings, todoModels.OrderBy(t => t.OrderIndex.Value).ToList());
        }

        private TodoModel SetupTodo(TodoModel todo)
        {
            todo.OrderIndex.Subscribe(this, _ => _allTodos.OrderBy(t => t.OrderIndex.Value));
            todo.IsVisible.Subscribe(this, _ => _allTodos.OrderBy(t => t.OrderIndex.Value));
            todo.IsDeleted.Subscribe(this, _ => _allTodos.Remove(TearDownTodo(todo)), false);
            return todo;
        }

        private TodoModel TearDownTodo(TodoModel todo)
        {
            todo.Unsubscribe(this);
            todo.Dispose();
            return todo;
        }

        public void AddNewTodo()
        {
            _allTodos.Add(SetupTodo(new TodoModel(_settings, new TodoJson(), true)));
        }

        public void MoveUp(TodoModel todo)
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

        public void MoveDown(TodoModel todo)
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

        protected override void DisposeModel()
        {
            foreach (var todo in AllTodos.Value)
                TearDownTodo(todo);

            MouseService.LeftButton.Unsubscribe(this);
        }
    }
}