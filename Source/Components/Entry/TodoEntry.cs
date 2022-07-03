using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly TodoHeader _header;

        public TodoEntry(Todo todo, int width)
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            HeightSizingMode = SizingMode.AutoSize;
            Width = width;

            _header = new TodoHeader(todo, width) { Parent = this };
        }

        protected override void DisposeControl()
        {
            _header.Dispose();
            base.DisposeControl();
        }
    }
}