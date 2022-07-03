using Blish_HUD.Controls;
using Blish_HUD.Input;

namespace TodoList.Components
{
    public sealed class TodoEntryHeader : FlowPanel
    {
        private readonly TodoCheckbox _checkbox;
        private readonly TodoTitle _todoTitle;
        private readonly Resources _resources;
        private readonly TodoCollapseArrow _collapseArrow;

        public TodoEntryHeader(Resources resources, Settings settings, int width)
        {
            _resources = resources;
            BackgroundTexture = resources.GetTexture(Textures.Header);
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox { Parent = this };
            _todoTitle = new TodoTitle(settings) { Parent = this };
            _collapseArrow = new TodoCollapseArrow(resources) { Parent = this };
            _todoTitle.Width = width - 2 * HEADER_HEIGHT;

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;
            Click += OnMouseClick;
        }

        private void OnMouseEntered(object target, MouseEventArgs args)
        {
            BackgroundTexture = _resources.GetTexture(Textures.HeaderHovered);
        }
        
        private void OnMouseLeft(object target, MouseEventArgs args)
        {
            BackgroundTexture = _resources.GetTexture(Textures.Header);
        }

        private void OnMouseClick(object target, MouseEventArgs args)
        {
            _collapseArrow.Expanded = !_collapseArrow.Expanded;
        }

        protected override void DisposeControl()
        {
            _checkbox.Dispose();
            _todoTitle.Dispose();
            _collapseArrow.Dispose();

            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            Click -= OnMouseClick;
            
            base.DisposeControl();
        }
    }
}