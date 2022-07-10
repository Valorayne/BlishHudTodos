using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditSchedule : FlowPanel
    {
        private readonly TodoEditRow _localTimeRow;
        private readonly TodoEditScheduleType _scheduleType;

        public TodoEditSchedule(Todo todo)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            
            _scheduleType = TodoEditRow.For(this, new TodoEditScheduleType(todo), "Reset Schedule",
                "Whether/when this task should automatically be reset");
            _localTimeRow = new TodoEditRow(new TodoEditLocalTime(todo), "Local Time", 
                "The local time at which this task should reset automatically") { Parent = this };

            UpdateLocalTimeRowVisibility();
            
            _scheduleType.ValueChanged += OnScheduleTypeChanged;
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            UpdateHeight();
            base.OnResized(e);
        }

        private void OnScheduleTypeChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateLocalTimeRowVisibility();
        }

        private void UpdateHeight()
        {
            if (_scheduleType != null)
                Height = _scheduleType.Height + (_localTimeRow.Visible ? _localTimeRow.Height : 0); 
        }

        private void UpdateLocalTimeRowVisibility()
        {
            _localTimeRow.Visible = _scheduleType.Selected?.Type == TodoScheduleType.LocalTime;
            UpdateHeight();
        }

        protected override void DisposeControl()
        {
            _scheduleType.ValueChanged -= OnScheduleTypeChanged;
            base.DisposeControl();
        }
    }
}