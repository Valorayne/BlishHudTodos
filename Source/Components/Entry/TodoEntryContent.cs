using System;
using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntryContent : FlowPanel
    {
        private readonly TodoCheckbox _checkbox;

        public event EventHandler<bool> VisibilityChanged;

        public TodoEntryContent(Todo todo)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox(todo) { Parent = this };
            new TodoScheduleIcon(todo) { Parent = this };
            new TodoDescription(todo) { Parent = this };

            _checkbox.Changed += OnCheckboxChanged;
        }

        private void OnCheckboxChanged(object sender, bool done)
        {
            if (done && !Settings.ShowAlreadyDoneTasks.Value)
            {
                Parent.Hide();
                VisibilityChanged?.Invoke(this, false);
            }
        }

        protected override void DisposeControl()
        {
            _checkbox.Changed -= OnCheckboxChanged;
            base.DisposeControl();
        }
    }
}