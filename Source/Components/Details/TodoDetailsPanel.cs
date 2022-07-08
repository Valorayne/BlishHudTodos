using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsPanel : FlowPanel
    {
        private readonly TodoDetailsInputArea _inputAreaArea;

        public TodoDetailsPanel(Todo todo, int width, int height)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;
            FlowDirection = ControlFlowDirection.SingleBottomToTop;

            var menuBar = new TodoDetailsMenuBar(todo, OnSave) { Parent = this };
            _inputAreaArea = new TodoDetailsInputArea(todo)
            {
                Parent = this,
                Height = height - menuBar.Height - TodoDetailsMenuBar.PADDING
            };
        }

        private void OnSave()
        {
            _inputAreaArea.Save();
            TodoDetailsWindowPool.Dispose();
        }
    }
}