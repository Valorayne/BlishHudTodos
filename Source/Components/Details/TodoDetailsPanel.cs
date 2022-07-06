using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsPanel : FlowPanel
    {
        private readonly Todo _todo;
        private readonly TodoDetailsInputArea _inputAreaArea;

        public TodoDetailsPanel(Todo todo, int width, int height)
        {
            _todo = todo;
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;
            FlowDirection = ControlFlowDirection.SingleBottomToTop;

            var menuBar = new TodoDetailsMenuBar(todo, OnSave) { Parent = this };
            _inputAreaArea = new TodoDetailsInputArea(todo, width)
            {
                Parent = this,
                Height = height - menuBar.Height - TodoDetailsMenuBar.PADDING
            };
        }

        private void OnSave()
        {
            _todo.Description = _inputAreaArea.Text;
            _todo.Schedule = _inputAreaArea.Schedule;
            _todo.Save();
            TodoDetailsWindowPool.Dispose();
        }
    }
}