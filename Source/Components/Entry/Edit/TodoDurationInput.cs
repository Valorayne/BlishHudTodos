using System;
using Blish_HUD.Controls;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoDurationInput : FlowPanel
    {
        private readonly TimeInput _days;
        private readonly TimeInput _hours;
        private readonly TimeInput _minutes;
        private readonly IProperty<TimeSpan> _updater;

        public TodoDurationInput(IVariable<TimeSpan> duration)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _days = new TimeInput((int)Math.Floor(duration.Value.TotalDays), "Days", 364) { Parent = this };
            _hours = new TimeInput(duration.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInput(duration.Value.Minutes, "Minutes", 59) { Parent = this };

            _updater = _days.Time.CombineWith(_hours.Time, _minutes.Time, (days, hours, minutes) => new TimeSpan(days, hours, minutes, 0))
                .Subscribe(this, v => duration.Value = v);
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
            _updater.Dispose();
            base.DisposeControl();
        }
    }
}