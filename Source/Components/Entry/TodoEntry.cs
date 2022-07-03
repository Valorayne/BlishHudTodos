using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Components.Body;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly TodoHeader _header;
        private TodoBody _body;

        public TodoEntry(int width)
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            HeightSizingMode = SizingMode.AutoSize;
            Width = width;

            _header = new TodoHeader(width) { Parent = this };

            _header.HeaderClicked += OnHeaderClicked;
        }

        private void OnHeaderClicked(object target, MouseEventArgs args)
        {
            if (_body != null)
            {
                _body.Dispose();
                _body = null;
            }
            else
            {
                _body = new TodoBody(Width) { Parent = this };
            }
        }

        protected override void DisposeControl()
        {
            _header.HeaderClicked -= OnHeaderClicked;
            _header.Dispose();
            _body?.Dispose();
            base.DisposeControl();
        }
    }
}