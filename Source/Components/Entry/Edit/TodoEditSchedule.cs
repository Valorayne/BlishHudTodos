using Blish_HUD.Controls;
using Todos.Source.Components.Generic;
using Todos.Source.Models;
using Todos.Source.Models.Resets;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditSchedule : FlowPanel
    {
        private readonly TodoModel _todo;
        private readonly TodoEditRow _localTimeRow;
        private readonly TodoEditScheduleType _scheduleType;
        private readonly TodoEditRow _durationRow;

        public TodoEditSchedule(TodoModel todo)
        {
            _todo = todo;
            
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            
            _scheduleType = TodoEditRow.For(this, new TodoEditScheduleType(todo.Schedule), "Reset Schedule",
                "Whether/when this task should automatically be reset");
            var localTimeInput = new TodoEditLocalTime(todo);
            _localTimeRow = new TodoEditRow(localTimeInput, "Local Time", 
                "The local time at which this task should reset automatically") { Parent = this };
            var durationInput = new TodoEditDuration(todo);
            _durationRow = new TodoEditRow(durationInput, "Duration",
                "The duration after which this task should reset automatically") { Parent = this };
            
            _todo.Schedule.Reset.Changed += OnScheduleTypeChanged;
            
            OnScheduleTypeChanged(todo.Schedule.Reset.Value);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            UpdateHeight();
            base.OnResized(e);
        }

        private void OnScheduleTypeChanged(IReset newValue)
        {
            _localTimeRow.Visible = newValue is LocalTimeReset;
            _durationRow.Visible = newValue is DurationReset;
            UpdateHeight();
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
            _todo.Schedule.Reset.Changed -= OnScheduleTypeChanged;
            base.DisposeControl();
        }
    }
}