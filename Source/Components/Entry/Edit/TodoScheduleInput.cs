using Blish_HUD.Controls;
using Todos.Source.Components.Generic;
using Todos.Source.Models;
using Todos.Source.Models.Resets;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoScheduleInput : FlowPanel
    {
        private readonly TodoScheduleModel _schedule;
        
        private readonly TodoInputRow _localTimeRow;
        private readonly TodoScheduleTypeInput _scheduleType;
        private readonly TodoInputRow _durationRow;

        public TodoScheduleInput(TodoScheduleModel schedule)
        {
            _schedule = schedule;
            
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            
            _scheduleType = TodoInputRow.For(this, new TodoScheduleTypeInput(schedule), "Reset Schedule",
                "Whether/when this task should automatically be reset");
            var localTimeInput = new TodoLocalTimeInput(schedule.LocalTime);
            _localTimeRow = new TodoInputRow(localTimeInput, "Local Time", 
                "The local time at which this task should reset automatically") { Parent = this };
            var durationInput = new TodoDurationInput(schedule.Duration);
            _durationRow = new TodoInputRow(durationInput, "Duration",
                "The duration after which this task should reset automatically") { Parent = this };
            
            _schedule.Reset.Subscribe(this, v =>
            {
                _localTimeRow.Visible = v is LocalTimeReset;
                _durationRow.Visible = v is DurationReset;
                UpdateHeight();
            });
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            UpdateHeight();
            base.OnResized(e);
        }

        private void UpdateHeight()
        {
            if (_scheduleType != null)
                Height = _scheduleType.Height
                         + (_localTimeRow.Visible ? _localTimeRow.Height : 0)
                         + (_durationRow.Visible ? _durationRow.Height : 0);
        }

        protected override void DisposeControl()
        {
            _schedule.Reset.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}