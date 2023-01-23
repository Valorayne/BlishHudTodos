using Blish_HUD.Controls;
using Todos.Source.Components.Generic;
using Todos.Source.Models;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Entry.Menu
{
    public sealed class TodoEditButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;
        private readonly TodoModel _todo;

        private readonly TodoListModel _todoList;

        public TodoEditButton(TodoListModel todoList, TodoModel todo) : base(
            Resources.GetTexture(Textures.EditIcon),
            Resources.GetTexture(Textures.EditIconHovered),
            WIDTH, WIDTH,
            _ => todo.IsEditing.Toggle())
        {
            _todoList = todoList;
            _todo = todo;
            _todo.IsEditing.Subscribe(this, inEditMode => BasicTooltipText = inEditMode ? "Stop Editing" : "Edit");
            _todoList.MovingTodo.Subscribe(this, move => Utility.Delay(() => Visible = move != todo));
        }

        protected override void DisposeControl()
        {
            _todo.Unsubscribe(this);
            _todoList.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}