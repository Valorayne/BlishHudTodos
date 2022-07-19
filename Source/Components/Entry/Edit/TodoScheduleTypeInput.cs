using Blish_HUD.Controls;
using Todos.Source.Models;
using Todos.Source.Models.Resets;

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
            
            foreach (var dropdownEntry in ResetFactory.AllDropdownEntries)
                Items.Add(dropdownEntry);
        }

        protected override void OnValueChanged(ValueChangedEventArgs e)
        {
            _schedule.ScheduleDropdown.Value = e.CurrentValue;
            BasicTooltipText = _schedule.Reset.Value.DropdownEntryTooltip;
            base.OnValueChanged(e);
        }
    }
}