using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoEditPanel : FlowPanel
    {
        private const int PADDING = 10;
        
        public TextBox Description { get; }
        
        private readonly Todo _todo;
        private readonly TodoEditSchedule _schedule;

        public TodoEditPanel(Todo todo)
        {
            _todo = todo;
            
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = Vector2.One * PADDING;
            
            Description = TodoEditRow.For(this, new TextBox { Text = todo.Task, Focused = true }, "Task");
            _schedule = TodoEditRow.For(this, new TodoEditSchedule(todo), "Reset Schedule");

            Description.TextChanged += OnChange;
            _schedule.ValueChanged += OnChange;
        }

        private void OnChange(object sender, EventArgs e)
        {
            _todo.Task = Description.Text;
            _todo.Schedule = _schedule.Selected;
            _todo.Save();
        }

        public void Focus()
        {
            Description.Focused = true;
        }
    }
}