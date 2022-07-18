using System;
using Blish_HUD.Controls;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoLocalTimeInput : FlowPanel
    {
        private readonly Variable<TimeSpan> _localTime;
        
        private readonly TimeInput _hours;
        private readonly TimeInput _minutes;

        public TodoLocalTimeInput(Variable<TimeSpan> localTime)
        {
            _localTime = localTime;
            
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _hours = new TimeInput(localTime.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInput(localTime.Value.Minutes, "Minutes", 59) { Parent = this };

            _hours.Time.Changed += OnTimeChanged;
            _minutes.Time.Changed += OnTimeChanged;
        }

        private void OnTimeChanged(int _)
        {
            _localTime.Value = TimeSpan.FromHours(_hours.Time.Value) + TimeSpan.FromMinutes(_minutes.Time.Value);
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