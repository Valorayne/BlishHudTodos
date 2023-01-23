using Blish_HUD.Controls;
using Blish_HUD.Input;
using Todos.Source.Models;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Entry.Menu
{
    public class TodoReorderButton : Panel
    {
        private readonly TodoModel _todo;
        private readonly TodoListModel _todoList;

        public TodoReorderButton(TodoListModel todoList, TodoModel todo)
        {
            _todoList = todoList;
            _todo = todo;

            Width = HEADER_HEIGHT;
            Height = HEADER_HEIGHT;
            BasicTooltipText = "Drag to reorder";

            BackgroundTexture = Resources.GetTexture(Textures.ReorderIcon);

            _todoList.MovingTodo.Subscribe(this, move => BasicTooltipText = move == todo ? null : "Drag to reorder");
        }

        protected override void OnLeftMouseButtonPressed(MouseEventArgs e)
        {
            if (_todoList.MovingTodo.IsUnset()) _todoList.MovingTodo.Set(_todo);
            base.OnLeftMouseButtonPressed(e);
        }

        protected override void DisposeControl()
        {
            _todoList.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}