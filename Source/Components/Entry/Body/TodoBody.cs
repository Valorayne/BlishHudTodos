using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components.Body
{
    public sealed class TodoBody : FlowPanel
    {
        private Label _label;
        
        public TodoBody(int width)
        {
            Width = width;
            HeightSizingMode = SizingMode.AutoSize;
            BackgroundColor = new Color(0, 0, 0, 0.2f);
        }

        private Label CreateLabel()
        {
            return new Label
            {
                Parent = this,
                Text = "Test Content"
            };
        }

        private bool _expanded;
        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (value)
                    _label = CreateLabel();
                else
                {
                    _label?.Dispose();
                    _label = null;
                }
                _expanded = value;
            }
        }

        protected override void DisposeControl()
        {
            _label?.Dispose();
            base.DisposeControl();
        }
    }
}