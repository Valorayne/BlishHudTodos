using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace Todos.Source.Components.Entry.Menu
{
    public sealed class TodoEntryHoverMenu : FlowPanel
    {
        private const int PADDING_RIGHT = 8;
        
        public TodoEditButton EditButton { get; }

        public TodoEntryHoverMenu(Action onEdit, Action<Point> onDelete)
        {
            Height = HEADER_HEIGHT;
            FlowDirection = ControlFlowDirection.SingleRightToLeft;
            OuterControlPadding = new Vector2(PADDING_RIGHT, 0);

            var deleteButton = new TodoDeleteButton(onDelete) { Parent = this };
            EditButton = new TodoEditButton(onEdit) { Parent = this };

            Width = deleteButton.Width + EditButton.Width + PADDING_RIGHT;
        }
    }
}