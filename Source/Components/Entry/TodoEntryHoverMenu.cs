using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntryHoverMenu : FlowPanel
    {
        private const int PADDING_RIGHT = 8;

        public TodoEntryHoverMenu(Todo todo)
        {
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleRightToLeft;
            OuterControlPadding = new Vector2(PADDING_RIGHT, 0);

            var deleteButton = new TodoDeleteButton(todo) { Parent = this };
            var editButton = new TodoEditButton(todo) { Parent = this };

            Width = deleteButton.Width + editButton.Width + PADDING_RIGHT;
        }
    }
}