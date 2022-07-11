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
    public sealed class TodoEditLocalTime : FlowPanel
    {
        private readonly TextBox _hours;
        private readonly TextBox _minutes;

        public event EventHandler<TimeSpan> ValueChanged;

        public TodoEditLocalTime(TodoModel todo)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;

            _hours = new TextBox
            {
                Parent = this,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (todo.Schedule.Value?.LocalTime.Hours ?? 0).ToString(),
                BasicTooltipText = "Hours (scroll to change)",
            };
            _minutes = new TextBox
            {
                Parent = this,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = (todo.Schedule.Value?.LocalTime.Minutes ?? 0).ToString(),
                BasicTooltipText = "Minutes (scroll to change)"
            };

            GameService.Input.Mouse.MouseWheelScrolled += OnScrolled;
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
        }

        public TimeSpan Time =>
            TimeSpan.FromHours(int.Parse(_hours.Text)) + TimeSpan.FromMinutes(int.Parse(_minutes.Text));

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
            _hours.Width = Width / 2;
            _minutes.Width = Width / 2;
            base.OnResized(e);
        }
        
        protected override void DisposeControl()
        {
            _hours.TextChanged -= OnHourChanged;
            _minutes.TextChanged -= OnMinutesChanged;
            base.DisposeControl();
        }
    }
}