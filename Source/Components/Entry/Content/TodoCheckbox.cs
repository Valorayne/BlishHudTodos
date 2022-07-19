using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoCheckbox : Panel
    {
        private readonly Point SIZE = new Point(32, 32);
        private readonly Point OFFSET = new Point(2, 2);
        
        private readonly TodoScheduleModel _schedule;
        private readonly Image _background;
        private readonly Image _hovered;
        private readonly Image _checked;

        public TodoCheckbox(TodoScheduleModel schedule)
        {
            _schedule = schedule;
            
            Width = HEADER_HEIGHT;
            Height = HEADER_HEIGHT;

            _background = new Image(CheckboxType.GetBackgroundImage()) { Parent = this, Location = OFFSET, Size = SIZE };
            _hovered = new Image(CheckboxType.GetHoveredImage()) { Parent = this, Location = OFFSET, Size = SIZE, Visible = false };
            _checked = new Image(CheckboxType.GetCheckedImage())
            {
                Parent = this, 
                Location = OFFSET, 
                Size = SIZE, 
                Visible = schedule.IsDone.Value, 
                BasicTooltipText = GetTooltipText(schedule)
            };
            
            schedule.IsDone.Subscribe(this, _ => UpdateState());
            
            Settings.CheckboxType.SettingChanged += OnCheckboxTypeChanged;
        }

        private static CheckboxType CheckboxType => Settings.CheckboxType.Value;

        private void OnCheckboxTypeChanged(object sender, ValueChangedEventArgs<CheckboxType> e)
        {
            _background.Texture = CheckboxType.GetBackgroundImage();
            _hovered.Texture = CheckboxType.GetHoveredImage();
            _checked.Texture = CheckboxType.GetCheckedImage();
        }

        protected override void OnMouseEntered(MouseEventArgs e)
        {
            _hovered.Visible = true;
            base.OnMouseEntered(e);
        }

        protected override void OnMouseLeft(MouseEventArgs e)
        {
            _hovered.Visible = false;
            base.OnMouseLeft(e);
        }

        private void UpdateState()
        {
            _checked.Visible = _schedule.IsDone.Value;
            _checked.BasicTooltipText = GetTooltipText(_schedule);
        }

        protected override void OnClick(MouseEventArgs e)
        {
            _schedule.ToggleDone();
            base.OnClick(e);
        }

        private string GetTooltipText(TodoScheduleModel todo)
        {
            return todo.IsDone.Value
                ? $"Done: {todo.LastExecution.Value?.ToDaysSinceString()}, {_schedule.LastExecution.Value?.ToShortTimeString()}" 
                : null;
        }

        protected override void DisposeControl()
        {
            _schedule.IsDone.Unsubscribe(this);
            Settings.CheckboxType.SettingChanged -= OnCheckboxTypeChanged;
            base.DisposeControl();
        }
    }
}