using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoTitle : Panel
    {
        private readonly Todo _todo;
        private readonly Label _label;
        
        public TodoTitle(Todo todo)
        {
            _todo = todo;
            Height = HEADER_HEIGHT;
            WidthSizingMode = SizingMode.AutoSize;
            _label = new Label
            {
                Parent = this,
                StrokeText = true,
                Text = todo.Text,
                AutoSizeWidth = true,
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
            base.DisposeControl();
        }
    }
}