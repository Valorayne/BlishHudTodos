using System;
using Blish_HUD.Controls;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditLocalTime : FlowPanel
    {
        public readonly Variable<TimeSpan> Time;
        
        private readonly TimeInputBox _hours;
        private readonly TimeInputBox _minutes;

        public TodoEditLocalTime(TodoModel todo)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _hours = new TimeInputBox(todo.ScheduleLocalTime.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInputBox(todo.ScheduleLocalTime.Value.Minutes, "Minutes", 59) { Parent = this };
            Time = new Variable<TimeSpan>(TimeSpan.FromHours(_hours.Time.Value) + TimeSpan.FromMinutes(_minutes.Time.Value));

            _hours.Time.Changed += OnTimeChanged;
            _minutes.Time.Changed += OnTimeChanged;
        }

        private void OnTimeChanged(int _)
        {
            Time.Value = TimeSpan.FromHours(_hours.Time.Value) + TimeSpan.FromMinutes(_minutes.Time.Value);
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