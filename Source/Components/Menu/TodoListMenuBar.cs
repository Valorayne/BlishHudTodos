using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Menu
{
    public sealed class TodoListMenuBar : FlowPanel
    {
        public const int HEIGHT = 40;

        public TodoListMenuBar(TodoListModel todoList)
        {
            FlowDirection = ControlFlowDirection.SingleRightToLeft;
            Height = HEIGHT;
            WidthSizingMode = SizingMode.Fill;
            OuterControlPadding = new Vector2(8, 0);

            new CloseTodoWindowButton { Parent = this };
            new TodoShowAlreadyDoneToggle { Parent = this };
            new AddNewTodoButton(todoList) { Parent = this };
        }
    }
}