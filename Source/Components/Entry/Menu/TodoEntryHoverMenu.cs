using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Menu
{
    public sealed class TodoEntryHoverMenu : FlowPanel
    {
        private const int PADDING_RIGHT = 8;

        public TodoEntryHoverMenu(TodoListModel todoList, TodoModel todo, Action<Point> onDelete)
        {
            Height = HEADER_HEIGHT;
            FlowDirection = ControlFlowDirection.SingleRightToLeft;
            OuterControlPadding = new Vector2(PADDING_RIGHT, 0);

            var deleteButton = new TodoDeleteButton(onDelete) { Parent = this };
            var editButton = new TodoEditButton(todo) { Parent = this };
            var reorderButton = new TodoReorderButton(todoList, todo) { Parent = this };

            Width = deleteButton.Width + editButton.Width + reorderButton.Width + PADDING_RIGHT;
        }
    }
}