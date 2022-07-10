using System;
using System.Linq;
using System.Windows.Forms;
using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.IdentityModel.Tokens;
using Todos.Source.Models;
using HorizontalAlignment = Blish_HUD.Controls.HorizontalAlignment;
using MouseEventArgs = Blish_HUD.Input.MouseEventArgs;
using TextBox = Blish_HUD.Controls.TextBox;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditDuration : FlowPanel
    {
        private readonly TextBox _days;
        private readonly TextBox _hours;
        private readonly TextBox _minutes;

        public event EventHandler<TimeSpan> ValueChanged;

        public TodoEditDuration(Todo todo)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _days = new TextBox
            {
                Parent = this,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = ((int) Math.Floor(todo.Schedule?.Duration.TotalDays ?? 0)).ToString(),
                BasicTooltipText = "Days (scroll to change)"
            };
            _hours = new TextBox
            {
                Parent = this,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (todo.Schedule?.Duration.Hours ?? 0).ToString(),
                BasicTooltipText = "Hours (scroll to change)",
            };
            _minutes = new TextBox
            {
                Parent = this,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (todo.Schedule?.Duration.Minutes ?? 0).ToString(),
                BasicTooltipText = "Minutes (scroll to change)"
            };

            GameService.Input.Mouse.MouseWheelScrolled += OnScrolled;
            _days.TextChanged += OnDaysChanged;
            _hours.TextChanged += OnHourChanged;
            _minutes.TextChanged += OnMinutesChanged;
        }

        private void OnScrolled(object sender, MouseEventArgs e)
        {
            var scrollValue = GameService.Input.Mouse.State.ScrollWheelValue / SystemInformation.MouseWheelScrollDelta; 
            if (_hours.MouseOver)
                _hours.Text = (int.Parse(_hours.Text) + scrollValue).ToString();
            if (_minutes.MouseOver)
                _minutes.Text = (int.Parse(_minutes.Text) + scrollValue).ToString();
            if (_days.MouseOver)
                _days.Text = (int.Parse(_days.Text) + scrollValue).ToString();
        }

        public TimeSpan Time =>
            new TimeSpan(int.Parse(_days.Text), int.Parse(_hours.Text), int.Parse(_minutes.Text), 0);

        private void OnDaysChanged(object sender, EventArgs e)
        {
            AssertValidity(_days, 364);
            ValueChanged?.Invoke(this, Time);
        }

        private void OnMinutesChanged(object sender, EventArgs e)
        {
            AssertValidity(_minutes, 59);
            ValueChanged?.Invoke(this, Time);
        }

        private void OnHourChanged(object sender, EventArgs e)
        {
            AssertValidity(_hours, 23);
            ValueChanged?.Invoke(this, Time);
        }

        private static void AssertValidity(TextInputBase textBox, int maxValue)
        {
            var digits = string.Join("", textBox.Text.Where(c => c >= '0' && c <= '9' || c == '-'));
            var number = int.Parse(digits.IsNullOrEmpty() ? "0" : digits);
            textBox.Text = Math.Max(Math.Min(number, maxValue), 0).ToString();
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
            _days.TextChanged -= OnDaysChanged;
            _hours.TextChanged -= OnHourChanged;
            _minutes.TextChanged -= OnMinutesChanged;
            base.DisposeControl();
        }
    }
}