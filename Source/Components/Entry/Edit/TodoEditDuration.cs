using System;
using System.Linq;
using System.Windows.Forms;
using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.IdentityModel.Tokens;
using Todos.Source.Models;
using Todos.Source.Utils;
using HorizontalAlignment = Blish_HUD.Controls.HorizontalAlignment;
using MouseEventArgs = Blish_HUD.Input.MouseEventArgs;
using TextBox = Blish_HUD.Controls.TextBox;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditDuration : FlowPanel
    {
        public readonly Variable<TimeSpan> Time;

        private readonly TimeInputBox _days;
        private readonly TimeInputBox _hours;
        private readonly TimeInputBox _minutes;

        public TodoEditDuration(TodoModel todo)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _days = new TimeInputBox((int)Math.Floor(todo.ScheduleDuration.Value.TotalDays), "Days", 364) { Parent = this };
            _hours = new TimeInputBox(todo.ScheduleDuration.Value.Hours, "Hours", 23) { Parent = this };
            _minutes = new TimeInputBox(todo.ScheduleDuration.Value.Minutes, "Minutes", 59) { Parent = this };
            Time = new Variable<TimeSpan>(new TimeSpan(_days.Time.Value, _hours.Time.Value, _minutes.Time.Value, 0));

            _days.Time.Changed += OnTimeChanged;
            _hours.Time.Changed += OnTimeChanged;
            _minutes.Time.Changed += OnTimeChanged;
        }

        private void OnTimeChanged(int _)
        {
            Time.Value = new TimeSpan(int.Parse(_days.Text), int.Parse(_hours.Text), int.Parse(_minutes.Text), 0);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            _days.Width = Width / 3;
            _hours.Width = Width / 3;
            _minutes.Width = Width / 3;
            base.OnResized(e);
        }
        
        protected override void DisposeControl()
        {
            _days.Time.Changed -= OnTimeChanged;
            _hours.Time.Changed -= OnTimeChanged;
            _minutes.Time.Changed -= OnTimeChanged;
            base.DisposeControl();
        }
    }
}