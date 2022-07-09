using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntryContent : FlowPanel
    {
        private readonly BackgroundTextureSubscription _hoverSubscription;
        private readonly TodoCheckbox _checkbox;
        private readonly TodoScheduleIcon _icon;
        
        public TodoDescription Description { get; }

        public TodoEntryContent(Todo todo)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox(todo) { Parent = this };
            _icon = new TodoScheduleIcon(todo) { Parent = this };
            Description = new TodoDescription(todo) { Parent = this };

            _hoverSubscription = new BackgroundTextureSubscription(this, Resources.GetTexture(Textures.Header),
                Resources.GetTexture(Textures.HeaderHovered));
            
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (Description != null)
                Description.Width = Width - _checkbox.Width - _icon.Width;
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            _hoverSubscription.Dispose();
            base.DisposeControl();
        }
    }
}