using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public class TodoEditSchedule : Dropdown
    {
        public TodoEditSchedule(Todo todo) : base()
        {
            Items.Add(TodoScheduleTypeExtensions.NO_RESET);
            Items.Add(TodoScheduleTypeExtensions.DAILY_SERVER_RESET);
            Items.Add(TodoScheduleTypeExtensions.WEEKLY_SERVER_RESET);

            SelectedItem = todo.Schedule.HasValue
                ? todo.Schedule.Value.Type.ToDropdownEntry()
                : TodoScheduleTypeExtensions.NO_RESET;
        }
        
        public TodoSchedule? Selected
        {
            get
            {
                var type = SelectedItem.FromDropdownEntry();
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