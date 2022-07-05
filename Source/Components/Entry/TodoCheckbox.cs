using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public class TodoCheckbox : Panel
    {
        private readonly Todo _todo;
        private readonly Checkbox _checkbox;

        public TodoCheckbox(Todo todo)
        {
            _todo = todo;
            Width = HEADER_HEIGHT;
            Height = HEADER_HEIGHT;
            
            _checkbox = new Checkbox
            {
                Parent = this, 
                Location = new Point(10, 10),
                Checked = todo.Done
            };

            _checkbox.CheckedChanged += OnClick;
        }

        private void OnClick(object sender, CheckChangedEvent e)
        {
            _todo.Done = e.Checked;
            _todo.Save();
        }

        protected override void DisposeControl()
        {
            _checkbox.CheckedChanged -= OnClick;
            _checkbox.Dispose();
            base.DisposeControl();
        }
    }
}