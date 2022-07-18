using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoScheduleIcon : Panel
    {
        public const int WIDTH = HEADER_HEIGHT;
        
        private readonly TodoModel _todo;
        private readonly Image _icon;

        public TodoScheduleIcon(TodoModel todo)
        {
            _todo = todo;
            Width = WIDTH;
            Height = WIDTH;
            _icon = new Image(IconTexture)
            {
                Parent = this,
                Location = new Point(0, 2),
                Width = 32,
                Height = 32,
                BasicTooltipText = GetIconTooltip()
            };

            todo.ScheduleType.Changed += OnScheduleTypeChanged;
            todo.ScheduleDuration.Changed += OnScheduleDetailsChanged;
            todo.ScheduleLocalTime.Changed += OnScheduleDetailsChanged;
            
            TimeService.NewMinute += OnNewMinute;
        }

        private void UpdateVisuals()
        {
            _icon.Texture = IconTexture;
            _icon.BasicTooltipText = GetIconTooltip();
        }

        private void OnScheduleDetailsChanged(TimeSpan newValue)
        {
            UpdateVisuals();
        }

        private void OnNewMinute(object sender, GameTime e)
        {
            UpdateVisuals();
        }

        private void OnScheduleTypeChanged(TodoScheduleType newValue)
        {
            UpdateVisuals();
        }

        private Texture2D IconTexture => _todo.ScheduleType.Value != TodoScheduleType.NoReset
            ? Resources.GetTexture(Textures.ScheduleIcon) 
            : Resources.GetTexture(Textures.Empty);

        private string GetIconTooltip()
        {
            switch (_todo.ScheduleType.Value)
            {
                case TodoScheduleType.NoReset: 
                    return null;
                case TodoScheduleType.DailyServer:
                    return $"Daily reset in {DateUtils.NextDailyReset.ToDurationString()}";
                case TodoScheduleType.WeeklyServer:
                    return $"Weekly reset in {DateUtils.NextWeeklyReset.ToDurationString()}";
                case TodoScheduleType.LocalTime:
                    return $"Local reset in {DateUtils.NextLocalReset(_todo.ScheduleLocalTime.Value).ToDurationString()}";
                case TodoScheduleType.Duration:
                    return $"Duration reset in {DateUtils.NextDurationReset(_todo.LastExecution, _todo.ScheduleDuration.Value).ToDurationString()}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void DisposeControl()
        {
            _todo.ScheduleType.Changed -= OnScheduleTypeChanged;
            _todo.ScheduleDuration.Changed -= OnScheduleDetailsChanged;
            _todo.ScheduleLocalTime.Changed -= OnScheduleDetailsChanged;
            TimeService.NewMinute -= OnNewMinute;
            base.DisposeControl();
        }
    }
}