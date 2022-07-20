using System;
using System.Linq;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xna.Framework;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Entry.Edit
{
    public class TimeInput : TextBox
    {
        private const int Y_DISTANCE_FOR_CHANGE = 10;

        public readonly IVariable<int> Time;

        private readonly int _maxValue;
        private readonly int _minValue;

        private Point? _dragStartPoint;
        private int? _dragStartValue;
        
        public TimeInput(int startValue, string name, int maxValue, int minValue = 0)
        {
            _maxValue = maxValue;
            _minValue = minValue;

            Time = Variables.Transient(startValue);
            HorizontalAlignment = HorizontalAlignment.Center;
            Text = startValue.ToString();
            BasicTooltipText = $"{name} (drag to change)";
            
            GameService.Input.Mouse.LeftMouseButtonReleased += OnLeftMouseReleased;
            GameService.Input.Mouse.MouseMoved += OnMouseMoved;

            TextChanged += OnTextChanged;
        }
        
        private void OnTextChanged(object sender, EventArgs e) => AssertValidity();
        private void OnLeftMouseReleased(object sender, MouseEventArgs e) => _dragStartPoint = null;

        protected override void OnLeftMouseButtonPressed(MouseEventArgs e)
        {
            _dragStartPoint = e.MousePosition;
            _dragStartValue = int.Parse(Text);
            base.OnLeftMouseButtonPressed(e);
        }

        private void OnMouseMoved(object sender, MouseEventArgs e)
        {
            if (_dragStartPoint.HasValue && _dragStartValue.HasValue)
            {
                var direction = e.MousePosition - _dragStartPoint.Value;
                var length = (int) direction.ToVector2().Length();
                var strongestPull = Math.Abs(direction.X) > Math.Abs(direction.Y) ? direction.X : -direction.Y;
                var sign = Math.Sign(strongestPull);
                var newHours = _dragStartValue.Value + sign * length / Y_DISTANCE_FOR_CHANGE;
                Text = newHours.ToString();
            }
        }

        private void AssertValidity()
        {
            var digits = string.Join("", Text.Where(c => c >= '0' && c <= '9' || c == '-'));
            var number = int.Parse(digits.IsNullOrEmpty() ? _minValue.ToString() : digits);
            Text = Math.Max(Math.Min(number, _maxValue), _minValue).ToString();
            Time.Value = int.Parse(Text);
        }

        protected override void DisposeControl()
        {
            Time.Dispose();
            TextChanged -= OnTextChanged;
            GameService.Input.Mouse.LeftMouseButtonReleased -= OnLeftMouseReleased;
            GameService.Input.Mouse.MouseMoved -= OnMouseMoved;
            base.DisposeControl();
        }
    }
}