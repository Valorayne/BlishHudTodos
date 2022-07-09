using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditPanel : FlowPanel
    {
        private const int PADDING = 10;
        
        private readonly Todo _todo;
        private readonly TodoEditSchedule _schedule;

        public TodoEditPanel(Todo todo)
        {
            _todo = todo;
            
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = Vector2.One * PADDING;
            
            _schedule = TodoEditRow.For(this, new TodoEditSchedule(todo), "Reset Schedule");

            _schedule.ValueChanged += OnChange;
        }

        private void OnChange(object sender, EventArgs e)
        {
            _todo.Schedule = _schedule.Selected;
            _todo.Save();
        }
    }
}