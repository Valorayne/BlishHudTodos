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

            todo.ScheduleChanged += OnScheduleChanged;
            TimeService.NewMinute += OnNewMinute;
        }

        private void OnNewMinute(object sender, GameTime e)
        {
            _icon.BasicTooltipText = GetIconTooltip();
        }

        private void OnScheduleChanged(TodoSchedule? schedule)
        {
            _icon.Texture = IconTexture;
            _icon.BasicTooltipText = GetIconTooltip();
        }

        private Texture2D IconTexture => _todo.Schedule.HasValue
            ? Resources.GetTexture(Textures.ScheduleIcon) 
            : Resources.GetTexture(Textures.Empty);

        private string GetIconTooltip()
        {
            if (!_todo.Schedule.HasValue)
                return null;

            switch (_todo.Schedule.Value.Type)
            {
                case TodoScheduleType.DailyServer:
                    return $"Daily reset in {DateUtils.NextDailyReset.ToDurationString()}";
                case TodoScheduleType.WeeklyServer:
                    return $"Weekly reset in {DateUtils.NextWeeklyReset.ToDurationString()}";
                case TodoScheduleType.LocalTime:
                    return $"Local reset in {DateUtils.NextLocalReset(_todo.Schedule.Value).ToDurationString()}";
                case TodoScheduleType.Duration:
                    return $"Duration reset in {DateUtils.NextDurationReset(_todo).ToDurationString()}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void DisposeControl()
        {
            _todo.ScheduleChanged -= OnScheduleChanged;
            TimeService.NewMinute -= OnNewMinute;
            base.DisposeControl();
        }
    }
}