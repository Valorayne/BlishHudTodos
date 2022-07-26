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
            foreach (var dropdownEntry in ResetFactory.AllDropdownEntries)
                Items.Add(dropdownEntry);

            _schedule.Reset.Subscribe(this, reset => BasicTooltipText = reset.DropdownEntryTooltip);
        }

        protected override void OnValueChanged(ValueChangedEventArgs e)
        {
            _schedule.ScheduleDropdown.Value = e.CurrentValue;
            base.OnValueChanged(e);
        }

        protected override void DisposeControl()
        {
            _schedule.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}