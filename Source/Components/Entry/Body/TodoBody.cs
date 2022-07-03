using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components.Body
{
    public sealed class TodoBody : FlowPanel
    {
        private readonly Label _label;
        
        public TodoBody(int width)
        {
            Width = width;
            HeightSizingMode = SizingMode.AutoSize;
            BackgroundColor = new Color(0, 0, 0, 0.2f);
            _label = CreateLabel();
        }

        private Label CreateLabel()
        {
            return new Label
            {
                Parent = this,
                Text = "Test Content"
            };
        }

        protected override void DisposeControl()
        {
            _label.Dispose();
            base.DisposeControl();
        }
    }
}