using Blish_HUD.Controls;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoScheduleIcon : Panel
    {
        public const int WIDTH = HEADER_HEIGHT;
        
        private readonly TodoScheduleModel _schedule;
        private readonly Image _icon;

        public TodoScheduleIcon(TodoScheduleModel schedule)
        {
            _schedule = schedule;
            Width = WIDTH;
            Height = WIDTH;
            _icon = new Image(IconTexture)
            {
                Parent = this,
                Location = new Point(0, 2),
                Width = 32,
                Height = 32,
                BasicTooltipText = _schedule.IconTooltip.Value
            };

            schedule.Reset.Subscribe(this, _ => UpdateVisuals());
            schedule.Duration.Subscribe(this, _ => UpdateVisuals());
            schedule.LocalTime.Subscribe(this, _ => UpdateVisuals());
            
            TimeService.NewMinute += OnNewMinute;
        }

        private void UpdateVisuals()
        {
            _icon.Texture = IconTexture;
            _icon.BasicTooltipText = _schedule.IconTooltip.Value;
        }

        private void OnNewMinute(object sender, GameTime e)
        {
            UpdateVisuals();
        }

        private Texture2D IconTexture => _schedule.IconTooltip.Value.IsNullOrEmpty()
            ? Resources.GetTexture(Textures.Empty) 
            : Resources.GetTexture(Textures.ScheduleIcon);

        protected override void DisposeControl()
        {
            _schedule.Reset.Unsubscribe(this);
            _schedule.Duration.Unsubscribe(this);
            _schedule.LocalTime.Unsubscribe(this);
            TimeService.NewMinute -= OnNewMinute;
            base.DisposeControl();
        }
    }
}