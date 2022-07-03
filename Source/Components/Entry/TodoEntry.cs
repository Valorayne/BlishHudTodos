using Blish_HUD.Controls;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly TodoHeader _header;

        public TodoEntry(Settings settings, int width)
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            HeightSizingMode = SizingMode.AutoSize;
            Width = width;

            _header = new TodoHeader(settings, width) { Parent = this };
        }

        protected override void DisposeControl()
        {
            _header.Dispose();
            base.DisposeControl();
        }
    }
}