using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Content
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
                Checked = todo.Done,
                BasicTooltipText = GetTooltipText(todo)
            };

            _checkbox.CheckedChanged += OnClick;
            Data.TodoModified += OnTodoModified;
            TimeService.NewMinute += CheckForChange;
        }

        private void OnTodoModified(object sender, Todo todo)
        {
            if (todo == _todo)
                UpdateState();
        }

        private void UpdateState()
        {
            _checkbox.Checked = _todo.Done;
            _checkbox.BasicTooltipText = GetTooltipText(_todo);
        }

        private void CheckForChange(object sender, GameTime e)
        {
            UpdateState();
        }

        private void OnClick(object sender, CheckChangedEvent e)
        {
            _todo.Done = e.Checked;
            _todo.Save();
            UpdateState();
        }

        private string GetTooltipText(Todo todo)
        {
            return todo.Done
                ? $"Done: {todo.LastExecution?.ToDaysSinceString()}, {_todo.LastExecution?.ToShortTimeString()}" 
                : null;
        }

        protected override void DisposeControl()
        {
            Data.TodoModified -= OnTodoModified;
            _checkbox.CheckedChanged -= OnClick;
            TimeService.NewMinute -= CheckForChange;
            base.DisposeControl();
        }
    }
}