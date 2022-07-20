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
        private readonly Image _hovered;

        public TodoCheckbox(TodoScheduleModel schedule)
        {
            _schedule = schedule;
            
            Width = HEADER_HEIGHT;
            Height = HEADER_HEIGHT;

            var background = new Image(CheckboxType.GetBackgroundImage()) { Parent = this, Location = OFFSET, Size = SIZE };
            _hovered = new Image(CheckboxType.GetHoveredImage()) { Parent = this, Location = OFFSET, Size = SIZE, Visible = false };
            var @checked = new Image(CheckboxType.GetCheckedImage())
            {
                Parent = this, 
                Location = OFFSET, 
                Size = SIZE, 
                Visible = schedule.IsDone.Value, 
                BasicTooltipText = GetTooltipText(schedule)
            };
            
            schedule.IsDone.Subscribe(this, isDone =>
            {
                @checked.Visible = isDone;
                @checked.BasicTooltipText = GetTooltipText(_schedule);
            });
            
            Settings.CheckboxType.Subscribe(this, _ =>
            {
                background.Texture = CheckboxType.GetBackgroundImage();
                _hovered.Texture = CheckboxType.GetHoveredImage();
                @checked.Texture = CheckboxType.GetCheckedImage();
            });
        }

        private static CheckboxType CheckboxType => Settings.CheckboxType.Value;

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
            Settings.CheckboxType.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}