using Blish_HUD.Controls;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly TodoEntryHeader _header;

        public TodoEntry(Resources resources, int width)
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            Width = width;
            HeightSizingMode = SizingMode.AutoSize;

            _header = new TodoEntryHeader(resources) { Parent = this };
        }

        protected override void DisposeControl()
        {
            _header.Dispose();
            base.DisposeControl();
        }
    }
}