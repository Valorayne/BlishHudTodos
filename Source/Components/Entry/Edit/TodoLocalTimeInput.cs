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

        public TodoLocalTimeInput(IVariable<TimeSpan> localTime)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _hours = new TimeInput(localTime.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInput(localTime.Value.Minutes, "Minutes", 59) { Parent = this };

            void OnTimeChanged(int _) => 
                localTime.Value = TimeSpan.FromHours(_hours.Time.Value) + TimeSpan.FromMinutes(_minutes.Time.Value);

            _hours.Time.Subscribe(this, OnTimeChanged);
            _minutes.Time.Subscribe(this, OnTimeChanged);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            _hours.Width = Width / 2;
            _minutes.Width = Width / 2;
            base.OnResized(e);
        }
        
        protected override void DisposeControl()
        {
            _hours.Time.Unsubscribe(this);
            _minutes.Time.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}