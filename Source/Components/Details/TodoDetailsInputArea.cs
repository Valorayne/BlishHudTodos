using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsInputArea : FlowPanel
    {
        private readonly TextBox _textBox;
        private readonly TodoScheduleTypeInput _schedule;

        public string Text => _textBox.Text;
        public TodoSchedule? Schedule => _schedule.Schedule;

        public TodoDetailsInputArea(Todo todo, int width)
        {
            WidthSizingMode = SizingMode.Fill;
            FlowDirection = ControlFlowDirection.TopToBottom;
            
            _textBox = new TextBox
            {
                Parent = this,
                Text = todo.Text,
                Width = width
            };

            _schedule = new TodoScheduleTypeInput(todo, width) { Parent = this };
        }

        protected override void DisposeControl()
        {
            _textBox.Dispose();
            _schedule.Dispose();
            base.DisposeControl();
        }
    }
}