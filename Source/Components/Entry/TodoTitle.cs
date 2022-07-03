using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public class TodoTitle : Panel
    {
        private readonly Label _label;
        
        public TodoTitle(Todo todo, int width)
        {
            Height = HEADER_HEIGHT;
            Width = width;
            _label = new Label
            {
                Parent = this,
                StrokeText = true,
                Text = todo.Text,
                Width = width,
                Location = new Point(0, 8)
            };
        }

        protected override void DisposeControl()
        {
            _label.Dispose();
            base.DisposeControl();
        }
    }
}