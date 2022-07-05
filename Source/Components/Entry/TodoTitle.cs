using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public class TodoTitle : Panel
    {
        private readonly Todo _todo;
        private readonly Label _label;
        
        public TodoTitle(Todo todo, int width)
        {
            _todo = todo;
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
            Data.TodoModified += OnTodoModified;
        }

        private void OnTodoModified(object sender, Todo todo)
        {
            if (todo == _todo)
                _label.Text = todo.Text;
        }

        protected override void DisposeControl()
        {
            Data.TodoModified -= OnTodoModified;
            _label.Dispose();
            base.DisposeControl();
        }
    }
}