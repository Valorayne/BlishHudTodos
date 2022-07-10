using System;
using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditSchedule : FlowPanel
    {
        private readonly Todo _todo;
        private readonly TodoEditRow _localTimeRow;
        private readonly TodoEditScheduleType _scheduleType;
        private readonly TodoEditLocalTime _localTimeInput;

        public TodoEditSchedule(Todo todo)
        {
            _todo = todo;
            
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            
            _scheduleType = TodoEditRow.For(this, new TodoEditScheduleType(todo), "Reset Schedule",
                "Whether/when this task should automatically be reset");
            _localTimeInput = new TodoEditLocalTime(todo);
            _localTimeRow = new TodoEditRow(_localTimeInput, "Local Time", 
                "The local time at which this task should reset automatically") { Parent = this };

            UpdateLocalTimeRowVisibility();
            
            _scheduleType.ValueChanged += OnScheduleTypeChanged;
            _localTimeInput.ValueChanged += OnLocalTimeChanged;
        }

        private void OnLocalTimeChanged(object sender, TimeSpan localTime)
        {
            _todo.Schedule = Selected;
            _todo.Save();
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            UpdateHeight();
            base.OnResized(e);
        }

        private void OnScheduleTypeChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateLocalTimeRowVisibility();
            _todo.Schedule = Selected;
            _todo.Save();
        }

        private void UpdateHeight()
        {
            if (_scheduleType != null)
                Height = _scheduleType.Height + (_localTimeRow.Visible ? _localTimeRow.Height : 0); 
        }
        
        public TodoSchedule? Selected
        {
            get
            {
                var type = _scheduleType.Selected;
                if (!type.HasValue)
                    return null;
                
                return new TodoSchedule
                {
                    Type = type.Value,
                    LocalTime = _localTimeInput.Time
                };
            }
        }

        private void UpdateLocalTimeRowVisibility()
        {
            _localTimeRow.Visible = _scheduleType.Selected == TodoScheduleType.LocalTime;
            UpdateHeight();
        }

        protected override void DisposeControl()
        {
            _scheduleType.ValueChanged -= OnScheduleTypeChanged;
            _localTimeInput.ValueChanged -= OnLocalTimeChanged;
            base.DisposeControl();
        }
    }
}