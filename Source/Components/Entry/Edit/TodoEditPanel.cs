using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditPanel : FlowPanel
    {
        private readonly TodoEditSchedule _schedule;
        public readonly TodoEditClipboardContent Clipboard;
        
        private const int PADDING = 10;

        public TodoEditPanel(TodoModel todo)
        {
            _schedule = new TodoEditSchedule(todo) { Parent = this };
            Clipboard = TodoEditRow.For(this, new TodoEditClipboardContent(todo), "Clipboard Content",
                "Content (e.g. map waypoints) to copy to your clipboard when clicking on this task");
            
            WidthSizingMode = SizingMode.Fill;
            UpdateHeight();
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = Vector2.One * PADDING;

            _schedule.Resized += OnScheduleResized;
        }

        private void OnScheduleResized(object sender, EventArgs eventArgs)
        {
            UpdateHeight();
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            UpdateHeight();
            base.OnResized(e);
        }

        private void UpdateHeight()
        {
            Height = _schedule.Height + PADDING + Clipboard.Height;
        }

        protected override void DisposeControl()
        {
            _schedule.Resized -= OnScheduleResized;
            base.DisposeControl();
        }
    }
}