using System;
using Blish_HUD.Controls;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoDurationInput : FlowPanel
    {
        private readonly TimeInput _days;
        private readonly TimeInput _hours;
        private readonly TimeInput _minutes;

        public TodoDurationInput(IVariable<TimeSpan> duration)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _days = new TimeInput((int)Math.Floor(duration.Value.TotalDays), "Days", 364) { Parent = this };
            _hours = new TimeInput(duration.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInput(duration.Value.Minutes, "Minutes", 59) { Parent = this };

            void OnTimeChanged(int _) => duration.Value = new TimeSpan(
                int.Parse(_days.Text), 
                int.Parse(_hours.Text), 
                int.Parse(_minutes.Text), 
                0
            );
            
            _days.Time.Subscribe(this, OnTimeChanged);
            _hours.Time.Subscribe(this, OnTimeChanged);
            _minutes.Time.Subscribe(this, OnTimeChanged);
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
            _days.Time.Unsubscribe(this);
            _hours.Time.Unsubscribe(this);
            _minutes.Time.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}