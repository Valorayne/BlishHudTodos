using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public class TodoCheckbox : Panel
    {
        private readonly Todo _todo;
        private readonly Checkbox _checkbox;

        public event EventHandler<bool> Changed;

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
                BasicTooltipText = GetTooltipText(todo.LastExecution)
            };

            _checkbox.CheckedChanged += OnClick;
        }

        private void OnClick(object sender, CheckChangedEvent e)
        {
            _todo.Done = e.Checked;
            _todo.Save();
            _checkbox.BasicTooltipText = GetTooltipText(_todo.LastExecution);
            Changed?.Invoke(this, e.Checked);
        }

        private string GetTooltipText(DateTime? lastExecution)
        {
            if (!lastExecution.HasValue)
                return null;
            
            var since = DateTime.Now - lastExecution.Value;
            return
                $"Last done: {GetDayString(since.Days)}, {_todo.LastExecution?.ToShortTimeString()}";
        }

        private static string GetDayString(int days)
        {
            switch (days)
            {
                case 0: return "today";
                case 1: return "yesterday";
                default: return $"{days} days ago";
            }
        }

        protected override void DisposeControl()
        {
            _checkbox.CheckedChanged -= OnClick;
            _checkbox.Dispose();
            base.DisposeControl();
        }
    }
}