using Blish_HUD.Controls;
using Blish_HUD.Input;

namespace TodoList.Components
{
    public sealed class TodoHeader : FlowPanel
    {
        private readonly TodoCheckbox _checkbox;
        private readonly TodoTitle _todoTitle;
        private readonly TodoCollapseArrow _collapseArrow;

        public TodoHeader(int width)
        {
            BackgroundTexture = Resources.GetTexture(Textures.Header);
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox { Parent = this };
            _todoTitle = new TodoTitle { Parent = this };
            _collapseArrow = new TodoCollapseArrow { Parent = this };
            _todoTitle.Width = width - 2 * HEADER_HEIGHT;

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;
            Click += OnMouseClick;
        }

        private void OnMouseEntered(object target, MouseEventArgs args)
        {
            BackgroundTexture = Resources.GetTexture(Textures.HeaderHovered);
        }
        
        private void OnMouseLeft(object target, MouseEventArgs args)
        {
            BackgroundTexture = Resources.GetTexture(Textures.Header);
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