using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components.Menu
{
    public sealed class TodoListMenuBar : FlowPanel
    {
        public const int HEIGHT = 40;
        
        private readonly AddNewTodoButton _addNewButton;
        private readonly TodoShowAlreadyDoneToggle _eyeButton;

        public TodoListMenuBar()
        {
            FlowDirection = ControlFlowDirection.SingleRightToLeft;
            Height = HEIGHT;
            WidthSizingMode = SizingMode.Fill;
            OuterControlPadding = new Vector2(10, 5);
            
            _addNewButton = new AddNewTodoButton { Parent = this };
            _eyeButton = new TodoShowAlreadyDoneToggle { Parent = this };
        }

        protected override void DisposeControl()
        {
            _addNewButton.Dispose();
            _eyeButton.Dispose();
            base.DisposeControl();
        }
    }
}