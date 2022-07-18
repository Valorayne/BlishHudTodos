using System;
using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditLocalTime : FlowPanel
    {
        private readonly TodoModel _todo;
        
        private readonly TimeInputBox _hours;
        private readonly TimeInputBox _minutes;

        public TodoEditLocalTime(TodoModel todo)
        {
            _todo = todo;
            
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _hours = new TimeInputBox(todo.Schedule.LocalTime.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInputBox(todo.Schedule.LocalTime.Value.Minutes, "Minutes", 59) { Parent = this };

            _hours.Time.Changed += OnTimeChanged;
            _minutes.Time.Changed += OnTimeChanged;
        }

        private void OnTimeChanged(int _)
        {
            _todo.Schedule.LocalTime.Value = TimeSpan.FromHours(_hours.Time.Value) + TimeSpan.FromMinutes(_minutes.Time.Value);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            _hours.Width = Width / 2;
            _minutes.Width = Width / 2;
            base.OnResized(e);
        }
        
        protected override void DisposeControl()
        {
            _hours.Time.Changed -= OnTimeChanged;
            _minutes.Time.Changed -= OnTimeChanged;
            base.DisposeControl();
        }
    }
}