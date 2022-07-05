using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components.Menu
{
    public sealed class TodoListMenuBar : FlowPanel
    {
        public const int HEIGHT = 40;

        public TodoListMenuBar()
        {
            FlowDirection = ControlFlowDirection.SingleRightToLeft;
            Height = HEIGHT;
            WidthSizingMode = SizingMode.Fill;
            OuterControlPadding = new Vector2(8, 0);
            
            new AddNewTodoButton { Parent = this };
            new TodoShowAlreadyDoneToggle { Parent = this };
        }
    }
}