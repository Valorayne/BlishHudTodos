using Blish_HUD.Controls;
using TodoList.Components.Menu;

namespace TodoList.Components
{
    public sealed class TodoListPanel : FlowPanel
    {
        private readonly TodoListMenuBar _menuBar;
        private readonly TodoScrollView _scrollView;

        public TodoListPanel()
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;
            
            _menuBar = new TodoListMenuBar { Parent = this };
            _scrollView = new TodoScrollView { Parent = this };
        }

        protected override void DisposeControl()
        {
            _menuBar.Dispose();
            _scrollView.Dispose();
            base.DisposeControl();
        }
    }
}