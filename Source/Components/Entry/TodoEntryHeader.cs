using Blish_HUD.Controls;
using Blish_HUD.Input;

namespace TodoList.Components
{
    public sealed class TodoEntryHeader : FlowPanel
    {
        private readonly TodoCheckbox _checkbox;
        private readonly Label _headerText;
        private readonly Resources _resources;

        public TodoEntryHeader(Resources resources)
        {
            _resources = resources;
            BackgroundTexture = resources.GetTexture(Textures.Header);
            WidthSizingMode = SizingMode.Fill;
            Height = HEADER_HEIGHT;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox { Parent = this };
            _headerText = new Label { Parent = this, Text = "Discover everything" };

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;
        }

        private void OnMouseEntered(object target, MouseEventArgs args)
        {
            BackgroundTexture = _resources.GetTexture(Textures.HeaderHovered);
        }
        
        private void OnMouseLeft(object target, MouseEventArgs args)
        {
            BackgroundTexture = _resources.GetTexture(Textures.Header);
        }

        protected override void DisposeControl()
        {
            _checkbox.Dispose();
            _headerText.Dispose();

            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            
            base.DisposeControl();
        }
    }
}