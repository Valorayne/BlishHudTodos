using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public class TodoEditScheduleType : Dropdown
    {
        public TodoEditScheduleType(TodoModel todo)
        {
            SelectedItem = todo.Schedule.HasValue
                ? todo.Schedule.Value.Type.ToDropdownEntry()
                : TodoScheduleTypeExtensions.NO_RESET;
            
            Items.Add(TodoScheduleTypeExtensions.NO_RESET);
            Items.Add(TodoScheduleTypeExtensions.DAILY_SERVER_RESET);
            Items.Add(TodoScheduleTypeExtensions.WEEKLY_SERVER_RESET);
            Items.Add(TodoScheduleTypeExtensions.LOCAL_TIME);
            Items.Add(TodoScheduleTypeExtensions.DURATION);

            BasicTooltipText = SelectedItem.GetTooltip();
        }

        protected override void OnValueChanged(ValueChangedEventArgs e)
        {
            BasicTooltipText = SelectedItem.GetTooltip();
            base.OnValueChanged(e);
        }

        public TodoScheduleType? Selected => SelectedItem?.FromDropdownEntry();
    }
}