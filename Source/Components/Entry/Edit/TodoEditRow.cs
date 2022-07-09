using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditRow : Panel
    {
        private readonly Label _label;
        public readonly Control _input;

        public static T For<T>(Container parent, T input, string description, string tooltip = null) where T : Control
        {
            return (T) new TodoEditRow(input, description, tooltip) { Parent = parent }._input;
        }
        
        public TodoEditRow(Control input, string description, string tooltip = null)
        {
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.Fill;

            _input = input;
            _label = new Label { Parent = this, Text = description, BasicTooltipText = tooltip, StrokeText = true };
            input.Parent = this;
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            _label.Location = new Point(0, 2);
            _label.Width = Width / 2;
            _input.Location = new Point(Width / 2, -1);
            _input.Width = Width / 2;
            base.OnResized(e);
        }
    }
}