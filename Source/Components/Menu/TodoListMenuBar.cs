using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Menu
{
    public sealed class TodoListMenuBar : FlowPanel
    {
        public const int HEIGHT = 40;

        public TodoListMenuBar(SettingsModel settings, TodoListModel todoList)
        {
            FlowDirection = ControlFlowDirection.SingleRightToLeft;
            Height = HEIGHT;
            WidthSizingMode = SizingMode.Fill;
            OuterControlPadding = new Vector2(8, 0);

            new CloseTodoWindowButton(settings) { Parent = this };
            new TodoShowAlreadyDoneToggle(settings) { Parent = this };
            new AddNewTodoButton(todoList) { Parent = this };
            new LockAllTasksToggle(settings) { Parent = this };
        }
    }
}