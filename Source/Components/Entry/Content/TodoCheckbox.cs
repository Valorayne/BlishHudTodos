using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoCheckbox : Panel
    {
        private readonly TodoModel _todo;
        private readonly Checkbox _checkbox;

        public TodoCheckbox(TodoModel todo)
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
            todo.DoneChanged += OnDoneChanged;
            TimeService.NewMinute += CheckForChange;
        }

        private void OnDoneChanged(bool newDone)
        {
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
        }

        private string GetTooltipText(TodoModel todo)
        {
            return todo.Done
                ? $"Done: {todo.LastExecution?.ToDaysSinceString()}, {_todo.LastExecution?.ToShortTimeString()}" 
                : null;
        }

        protected override void DisposeControl()
        {
            _todo.DoneChanged -= OnDoneChanged;
            _checkbox.CheckedChanged -= OnClick;
            TimeService.NewMinute -= CheckForChange;
            base.DisposeControl();
        }
    }
}