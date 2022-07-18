using System;
using Blish_HUD.Controls;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoDurationInput : FlowPanel
    {
        private readonly Variable<TimeSpan> _duration;
        
        private readonly TimeInput _days;
        private readonly TimeInput _hours;
        private readonly TimeInput _minutes;

        public TodoDurationInput(Variable<TimeSpan> duration)
        {
            _duration = duration;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _days = new TimeInput((int)Math.Floor(duration.Value.TotalDays), "Days", 364) { Parent = this };
            _hours = new TimeInput(duration.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInput(duration.Value.Minutes, "Minutes", 59) { Parent = this };

            _days.Time.Changed += OnTimeChanged;
            _hours.Time.Changed += OnTimeChanged;
            _minutes.Time.Changed += OnTimeChanged;
        }

        private void OnTimeChanged(int _)
        {
            _duration.Value = new TimeSpan(int.Parse(_days.Text), int.Parse(_hours.Text), int.Parse(_minutes.Text), 0);
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