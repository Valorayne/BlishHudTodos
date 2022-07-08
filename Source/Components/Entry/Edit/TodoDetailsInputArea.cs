using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsInputArea : FlowPanel
    {
        private const int PADDING = 5;
        
        private readonly Todo _todo;
        private readonly TextBox _description;
        private readonly Dropdown _schedule;

        public TodoDetailsInputArea(Todo todo)
        {
            _todo = todo;
            
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = Vector2.One * PADDING;
            
            _description = TodoDetailsRow.For(this, new TextBox { Text = todo.Description, Focused = true }, "Description");
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

            _description.TextChanged += OnChange;
            _schedule.ValueChanged += OnChange;
        }

        private void OnChange(object sender, EventArgs e)
        {
            _todo.Description = _description.Text;
            _todo.Schedule = Schedule;
            _todo.Save();
        }

        public void Focus()
        {
            _description.Focused = true;
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