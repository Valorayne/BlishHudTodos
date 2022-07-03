using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsPanel : FlowPanel
    { 
        private readonly TodoDetailsInputArea _inputAreaArea;
        private readonly TodoDetailsMenuBar _menuBar;

        public TodoDetailsPanel(Todo todo, bool isNew, int width, int height)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;
            FlowDirection = ControlFlowDirection.SingleBottomToTop;

            _menuBar = new TodoDetailsMenuBar(todo, isNew) { Parent = this };
            _inputAreaArea = new TodoDetailsInputArea(todo, width) { Parent = this, Height = height - _menuBar.Height - 2* 5 };
        }

        protected override void DisposeControl()
        {
            _inputAreaArea.Dispose();
            _menuBar.Dispose();
            base.DisposeControl();
        }
    }
}