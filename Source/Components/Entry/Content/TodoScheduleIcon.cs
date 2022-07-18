using System;
using Blish_HUD.Controls;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todos.Source.Models;
using Todos.Source.Models.Resets;
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
                BasicTooltipText = _todo.IconTooltip
            };

            todo.Schedule.Reset.Changed += OnScheduleTypeChanged;
            todo.Schedule.Duration.Changed += OnScheduleDetailsChanged;
            todo.Schedule.LocalTime.Changed += OnScheduleDetailsChanged;
            
            TimeService.NewMinute += OnNewMinute;
        }

        private void UpdateVisuals()
        {
            _icon.Texture = IconTexture;
            _icon.BasicTooltipText = _todo.IconTooltip;
        }

        private void OnScheduleDetailsChanged(TimeSpan newValue)
        {
            UpdateVisuals();
        }

        private void OnNewMinute(object sender, GameTime e)
        {
            UpdateVisuals();
        }

        private void OnScheduleTypeChanged(IReset newValue)
        {
            UpdateVisuals();
        }

        private Texture2D IconTexture => _todo.IconTooltip.IsNullOrEmpty()
            ? Resources.GetTexture(Textures.Empty) 
            : Resources.GetTexture(Textures.ScheduleIcon);

        protected override void DisposeControl()
        {
            _todo.Schedule.Reset.Changed -= OnScheduleTypeChanged;
            _todo.Schedule.Duration.Changed -= OnScheduleDetailsChanged;
            _todo.Schedule.LocalTime.Changed -= OnScheduleDetailsChanged;
            TimeService.NewMinute -= OnNewMinute;
            base.DisposeControl();
        }
    }
}