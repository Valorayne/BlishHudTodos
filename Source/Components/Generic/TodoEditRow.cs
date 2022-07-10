using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditRow : Panel
    {
        public Label Label { get; }
        public Control Control { get; }

        public static T For<T>(Container parent, T input, string description, string tooltip = null) where T : Control
        {
            return (T) new TodoEditRow(input, description, tooltip) { Parent = parent }.Control;
        }
        
        public TodoEditRow(Control control, string description, string tooltip = null)
        {
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.Fill;

            Control = control;
            Label = new Label { Parent = this, Text = description, BasicTooltipText = tooltip, StrokeText = true };
            control.Parent = this;
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            Label.Location = new Point(0, 2);
            Label.Width = Width / 2;
            Control.Location = new Point(Width / 2, VerticalOffset);
            Control.Width = Width / 2;
            base.OnResized(e);
        }

        private int VerticalOffset
        {
            get
            {
                if (Control is Checkbox)
                    return 3;
                return -1;
            }
        }
    }
}