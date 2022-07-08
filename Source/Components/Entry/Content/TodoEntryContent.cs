using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntryContent : FlowPanel
    {
        private readonly BackgroundTextureSubscription _hoverSubscription;
        
        public TodoEntryContent(Todo todo)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            new TodoCheckbox(todo) { Parent = this };
            new TodoScheduleIcon(todo) { Parent = this };
            new TodoTaskLabel(todo) { Parent = this };

            _hoverSubscription = new BackgroundTextureSubscription(this, Resources.GetTexture(Textures.Header),
                Resources.GetTexture(Textures.HeaderHovered));
        }
        
        protected override void DisposeControl()
        {
            _hoverSubscription.Dispose();
            base.DisposeControl();
        }
    }
}