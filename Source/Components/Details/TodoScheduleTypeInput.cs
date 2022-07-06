using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoScheduleTypeInput : FlowPanel
    {
        private readonly Todo _todo;
        private readonly Dropdown _scheduleType;
        private readonly Label _label;

        public TodoScheduleTypeInput(Todo todo, int width)
        {
            _todo = todo;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            Width = width;
            HeightSizingMode = SizingMode.AutoSize;
            
            _label = new Label
            {
                Parent = this,
                Text = "Reset Schedule:",
                Width = width / 2
            };

            _scheduleType = new Dropdown
            {
                Parent = this,
                Items =
                {
                    TodoScheduleTypeExtensions.NO_RESET,
                    TodoScheduleTypeExtensions.DAILY_SERVER_RESET,
                    TodoScheduleTypeExtensions.WEEKLY_SERVER_RESET
                },
                Width = width / 2,
                SelectedItem = todo.Schedule.HasValue ? todo.Schedule.Value.Type.ToDropdownEntry() : TodoScheduleTypeExtensions.NO_RESET
            };
        }

        public TodoSchedule? Schedule
        {
            get
            {
                var type = _scheduleType.SelectedItem.FromDropdownEntry();
                if (!type.HasValue)
                    return null;
                
                return new TodoSchedule
                {
                    Type = type.Value
                };
            }
        }

        protected override void DisposeControl()
        {
            _scheduleType.Dispose();
            _label.Dispose();
            base.DisposeControl();
        }
    }
}