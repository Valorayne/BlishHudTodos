using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public sealed class TodoEntryHoverMenu : FlowPanel
    {
        private const int PADDING_RIGHT = 8;
        
        public TodoEditButton EditButton { get; }

        public TodoEntryHoverMenu(Action onEdit, Action onDelete)
        {
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleRightToLeft;
            OuterControlPadding = new Vector2(PADDING_RIGHT, 0);

            var deleteButton = new TodoDeleteButton(onDelete) { Parent = this };
            EditButton = new TodoEditButton(onEdit) { Parent = this };

            Width = deleteButton.Width + EditButton.Width + PADDING_RIGHT;
        }
    }
}