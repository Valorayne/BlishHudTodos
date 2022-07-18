using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public class TodoScheduleTypeInput : Dropdown
    {
        private readonly TodoScheduleModel _schedule;

        public TodoScheduleTypeInput(TodoScheduleModel schedule)
        {
            _schedule = schedule;
            
            SelectedItem = schedule.Reset.Value.DropdownEntry;
            BasicTooltipText = schedule.Reset.Value.DropdownEntryTooltip;
            
            Items.Add(TodoScheduleModel.NO_RESET);
            Items.Add(TodoScheduleModel.DAILY_SERVER);
            Items.Add(TodoScheduleModel.WEEKLY_SERVER);
            Items.Add(TodoScheduleModel.LOCAL_TIME);
            Items.Add(TodoScheduleModel.DURATION);
        }

        protected override void OnValueChanged(ValueChangedEventArgs e)
        {
            _schedule.UpdateSchedule(e.CurrentValue);
            BasicTooltipText = _schedule.Reset.Value.DropdownEntryTooltip;
            base.OnValueChanged(e);
        }
    }
}