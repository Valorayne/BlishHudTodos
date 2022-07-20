using Blish_HUD.Controls;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Models;
using Todos.Source.Utils;
using Todos.Source.Utils.Subscriptions;

namespace Todos.Source.Components.Entry.Content
{
    public sealed class TodoEntryContent : FlowPanel
    {
        private const int PADDING_RIGHT = 5;
        
        private readonly BackgroundTextureSubscription _hoverSubscription;
        private readonly TodoCheckbox _checkbox;
        private readonly TodoScheduleIcon _icon;
        private readonly TodoDescription _description;

        public TodoEntryContent(SettingsModel settings, TodoModel todo, TodoEntryHoverMenu hoverMenu)
        {
            WidthSizingMode = SizingMode.Fill;
            Height = HEADER_HEIGHT;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox(settings, todo.Schedule) { Parent = this };
            _icon = new TodoScheduleIcon(todo.Schedule) { Parent = this };
            _description = new TodoDescription(todo, hoverMenu) { Parent = this };

            _hoverSubscription = new BackgroundTextureSubscription(this, 
                Resources.GetTexture(Textures.Header),
                Resources.GetTexture(Textures.HeaderHovered));
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (_description != null)
                _description.Width = Width - _checkbox.Width - _icon.Width - PADDING_RIGHT;
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            _hoverSubscription.Dispose();
            base.DisposeControl();
        }
    }
}