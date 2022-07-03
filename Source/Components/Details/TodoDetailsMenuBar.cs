using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsMenuBar : FlowPanel
    {
        private const int PADDING = 5;
        
        private readonly Todo _todo;
        private readonly StandardButton _saveButton;

        public TodoDetailsMenuBar(Todo todo, bool isNew)
        {
            _todo = todo;
            
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.RightToLeft;
            Padding = new Thickness(PADDING);

            _saveButton = new StandardButton
            {
                Parent = this,
                Text = isNew ? "Create" : "Save",
                Width = 100
            };
        }
        
        private void OnButtonClicked(object target, MouseEventArgs args)
        {
            TodoDetailsWindowPool.Spawn(args.MousePosition, _todo);
        }

        protected override void DisposeControl()
        {
            _saveButton.Dispose();
            base.DisposeControl();
        }
    }
}