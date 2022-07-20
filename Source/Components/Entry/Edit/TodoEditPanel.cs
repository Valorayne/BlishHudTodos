using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditPanel : FlowPanel
    {
        private readonly TodoScheduleInput _schedule;
        private readonly TodoClipboardContentInput _clipboard;
        
        private const int PADDING = 10;

        public TodoEditPanel(TodoModel todo)
        {
            WidthSizingMode = SizingMode.Fill;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = Vector2.One * PADDING;
            
            _schedule = new TodoScheduleInput(todo.Schedule) { Parent = this };
            _clipboard = TodoEditRow.For(this, new TodoClipboardContentInput(todo), "Clipboard Content",
                "Content (e.g. map waypoints) to copy to your clipboard when clicking on this task");

            _schedule.Resized += OnScheduleResized;
        }

        private void UpdateHeight() => Height = _schedule.Height + PADDING + _clipboard.Height;
        private void OnScheduleResized(object sender, EventArgs eventArgs) => UpdateHeight();

        protected override void OnResized(ResizedEventArgs e)
        {
            UpdateHeight();
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            _schedule.Resized -= OnScheduleResized;
            base.DisposeControl();
        }
    }
}