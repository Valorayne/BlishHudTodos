using System;
using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditDuration : FlowPanel
    {
        private readonly TodoModel _todo;
        
        private readonly TimeInputBox _days;
        private readonly TimeInputBox _hours;
        private readonly TimeInputBox _minutes;

        public TodoEditDuration(TodoModel todo)
        {
            _todo = todo;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _days = new TimeInputBox((int)Math.Floor(todo.Schedule.Duration.Value.TotalDays), "Days", 364) { Parent = this };
            _hours = new TimeInputBox(todo.Schedule.Duration.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInputBox(todo.Schedule.Duration.Value.Minutes, "Minutes", 59) { Parent = this };

            _days.Time.Changed += OnTimeChanged;
            _hours.Time.Changed += OnTimeChanged;
            _minutes.Time.Changed += OnTimeChanged;
        }

        private void OnTimeChanged(int _)
        {
            _todo.Schedule.Duration.Value = new TimeSpan(int.Parse(_days.Text), int.Parse(_hours.Text), int.Parse(_minutes.Text), 0);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            _days.Width = Width / 3;
            _hours.Width = Width / 3;
            _minutes.Width = Width / 3;
            base.OnResized(e);
        }
        
        protected override void DisposeControl()
        {
            _days.Time.Changed -= OnTimeChanged;
            _hours.Time.Changed -= OnTimeChanged;
            _minutes.Time.Changed -= OnTimeChanged;
            base.DisposeControl();
        }
    }
}