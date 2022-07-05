using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsPanel : FlowPanel
    {
        private readonly Todo _todo;
        private readonly TodoDetailsInputArea _inputAreaArea;
        private readonly TodoDetailsMenuBar _menuBar;

        public TodoDetailsPanel(Todo todo, int width, int height)
        {
            _todo = todo;
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;
            FlowDirection = ControlFlowDirection.SingleBottomToTop;

            _menuBar = new TodoDetailsMenuBar(todo) { Parent = this };
            _inputAreaArea = new TodoDetailsInputArea(todo, width)
            {
                Parent = this, 
                Height = height - _menuBar.Height - TodoDetailsMenuBar.PADDING
            };

            _menuBar.SaveButton.Click += OnSave;
        }
        
        private void OnSave(object target, MouseEventArgs args)
        {
            _todo.Text = _inputAreaArea.Text;
            _todo.Save();
            TodoDetailsWindowPool.Dispose();
        }

        protected override void DisposeControl()
        {
            _menuBar.SaveButton.Click -= OnSave;
            _inputAreaArea.Dispose();
            _menuBar.Dispose();
            base.DisposeControl();
        }
    }
}