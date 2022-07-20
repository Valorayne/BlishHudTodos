using System;
using Blish_HUD.Controls;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoLocalTimeInput : FlowPanel
    {
        private readonly TimeInput _hours;
        private readonly TimeInput _minutes;
        private readonly IProperty<TimeSpan> _updater;

        public TodoLocalTimeInput(IVariable<TimeSpan> localTime)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _hours = new TimeInput(localTime.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInput(localTime.Value.Minutes, "Minutes", 59) { Parent = this };

            _updater = _hours.Time.CombineWith(_minutes.Time, (hours, minutes) => new TimeSpan(0, hours, minutes, 0))
                .Subscribe(this, v => localTime.Value = v);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            _hours.Width = Width / 2;
            _minutes.Width = Width / 2;
            base.OnResized(e);
        }
        
        protected override void DisposeControl()
        {
            _updater.Dispose();
            base.DisposeControl();
        }
    }
}