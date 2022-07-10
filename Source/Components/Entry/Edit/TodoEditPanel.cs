﻿using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditPanel : FlowPanel
    {
        private const int PADDING = 10;

        public TodoEditPanel(Todo todo)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = Vector2.One * PADDING;
            
            TodoEditRow.For(this, new TodoEditScheduleType(todo), "Reset Schedule",
                "Whether/when this task should automatically be reset");
            TodoEditRow.For(this, new TodoEditClipboardContent(todo), "Clipboard Content",
                "Content (e.g. map waypoints) to copy to your clipboard when clicking on this task");
        }
    }
}