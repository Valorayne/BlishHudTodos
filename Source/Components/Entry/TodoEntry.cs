using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Components.Body;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly TodoHeader _header;
        private readonly TodoBody _body;

        public TodoEntry(int width)
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            HeightSizingMode = SizingMode.AutoSize;
            Width = width;

            _header = new TodoHeader(width) { Parent = this };
            _body = new TodoBody(width) { Parent = this };

            _header.HeaderClicked += OnHeaderClicked;
        }

        private void OnHeaderClicked(object target, MouseEventArgs args)
        {
            _body.Expanded = !_body.Expanded;
        }

        protected override void DisposeControl()
        {
            _header.HeaderClicked -= OnHeaderClicked;
            _header.Dispose();
            _body.Dispose();
            base.DisposeControl();
        }
    }
}