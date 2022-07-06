using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TodoList.Models;

namespace TodoList.Components
{
    public class TodoScheduleIcon : Panel
    {
        public const int WIDTH = HEADER_HEIGHT;
        
        private readonly Todo _todo;
        private readonly Image _icon;

        public TodoScheduleIcon(Todo todo)
        {
            _todo = todo;
            Width = WIDTH;
            Height = WIDTH;
            _icon = new Image(IconTexture)
            {
                Parent = this,
                Location = new Point(0, 2),
                BasicTooltipText = GetIconTooltip()
            };

            Data.TodoModified += OnTodoModified;
        }

        private void OnTodoModified(object sender, Todo todo)
        {
            if (todo == _todo)
            {
                _icon.Texture = IconTexture;
                _icon.BasicTooltipText = GetIconTooltip();
            }
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
                case TodoScheduleType.CustomLocal:
                    return "Bla";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void DisposeControl()
        {
            Data.TodoModified -= OnTodoModified;
            base.DisposeControl();
        }
    }
}