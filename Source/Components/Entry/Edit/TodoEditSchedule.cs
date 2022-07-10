using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public class TodoEditSchedule : Dropdown
    {
        private readonly Todo _todo;

        public TodoEditSchedule(Todo todo)
        {
            _todo = todo;
            
            SelectedItem = todo.Schedule.HasValue
                ? todo.Schedule.Value.Type.ToDropdownEntry()
                : TodoScheduleTypeExtensions.NO_RESET;
            
            Items.Add(TodoScheduleTypeExtensions.NO_RESET);
            Items.Add(TodoScheduleTypeExtensions.DAILY_SERVER_RESET);
            Items.Add(TodoScheduleTypeExtensions.WEEKLY_SERVER_RESET);

            BasicTooltipText = SelectedItem.GetTooltip();
        }

        protected override void OnValueChanged(ValueChangedEventArgs e)
        {
            BasicTooltipText = SelectedItem.GetTooltip();
            _todo.Schedule = Selected;
            _todo.Save();
            base.OnValueChanged(e);
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