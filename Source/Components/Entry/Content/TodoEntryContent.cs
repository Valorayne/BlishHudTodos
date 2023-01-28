using Blish_HUD.Controls;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Content
{
    public sealed class TodoEntryContent : FlowPanel
    {
        private const int PADDING_RIGHT = 5;
        private readonly TodoCheckbox _checkbox;
        private readonly TodoDescription _description;

        private readonly TodoScheduleIcon _icon;
        private readonly TodoListModel _todoList;

        public TodoEntryContent(SettingsModel settings, TodoModel todo, TodoListModel todoList,
            TodoEntryHoverMenu hoverMenu)
        {
            _todoList = todoList;

            WidthSizingMode = SizingMode.Fill;
            Height = HEADER_HEIGHT;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox(settings, todo.Schedule) { Parent = this };
            _icon = new TodoScheduleIcon(todo.Schedule) { Parent = this };
            _description = new TodoDescription(todo, hoverMenu) { Parent = this };

            _todoList.HoveredTodo.Subscribe(this, hovered =>
            {
                BackgroundTexture = hovered == todo
                    ? Resources.GetTexture(Textures.HeaderHovered)
                    : Resources.GetTexture(Textures.Header);
            });
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (_description != null)
                _description.Width = Width - _checkbox.Width - _icon.Width - PADDING_RIGHT;
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            _todoList.HoveredTodo.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}