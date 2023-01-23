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
        private readonly TodoModel _todo;

        public TodoEntryContent(SettingsModel settings, TodoModel todo, TodoEntryHoverMenu hoverMenu)
        {
            _todo = todo;

            WidthSizingMode = SizingMode.Fill;
            Height = HEADER_HEIGHT;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox(settings, todo.Schedule) { Parent = this };
            _icon = new TodoScheduleIcon(todo.Schedule) { Parent = this };
            _description = new TodoDescription(todo, hoverMenu) { Parent = this };

            _todo.IsHovered.Subscribe(this, isHovered =>
            {
                BackgroundTexture = isHovered
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
            _todo.IsHovered.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}