using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsInputArea : FlowPanel
    {
        private readonly Todo _todo;
        private readonly TextBox _textBox;
        private readonly Dropdown _schedule;

        public TodoDetailsInputArea(Todo todo)
        {
            _todo = todo;
            
            WidthSizingMode = SizingMode.Fill;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            
            _textBox = TodoDetailsRow.For(this, new TextBox { Text = todo.Description, Focused = true }, "Description");
            _schedule = TodoDetailsRow.For(this, new Dropdown
            {
                Items =
                {
                    TodoScheduleTypeExtensions.NO_RESET,
                    TodoScheduleTypeExtensions.DAILY_SERVER_RESET,
                    TodoScheduleTypeExtensions.WEEKLY_SERVER_RESET
                },
                SelectedItem = todo.Schedule.HasValue ? todo.Schedule.Value.Type.ToDropdownEntry() : TodoScheduleTypeExtensions.NO_RESET
            }, "Reset Schedule");
        }

        public void Save()
        {
            _todo.Description = _textBox.Text;
            _todo.Schedule = Schedule;
            _todo.Save();
        }
        
        private TodoSchedule? Schedule
        {
            get
            {
                var type = _schedule.SelectedItem.FromDropdownEntry();
                if (!type.HasValue)
                    return null;
                
                return new TodoSchedule
                {
                    Type = type.Value
                };
            }
        }
    }
}