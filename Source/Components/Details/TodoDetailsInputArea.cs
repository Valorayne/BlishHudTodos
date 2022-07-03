using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsInputArea : FlowPanel
    {
        private readonly Todo _todo;
        private readonly TextBox _textBox;

        public TodoDetailsInputArea(Todo todo, int width)
        {
            _todo = todo;
            WidthSizingMode = SizingMode.Fill;
            FlowDirection = ControlFlowDirection.TopToBottom;
            
            _textBox = new TextBox
            {
                Parent = this,
                Text = todo.Text,
                Width = width
            };
        }

        protected override void DisposeControl()
        {
            _textBox.Dispose();
            base.DisposeControl();
        }
    }
}