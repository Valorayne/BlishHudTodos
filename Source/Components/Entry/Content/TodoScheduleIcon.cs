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

            todo.Schedule.Reset.Subscribe(this, _ => UpdateVisuals());
            todo.Schedule.Duration.Subscribe(this, _ => UpdateVisuals());
            todo.Schedule.LocalTime.Subscribe(this, _ => UpdateVisuals());
            
            TimeService.NewMinute += OnNewMinute;
        }

        private void UpdateVisuals()
        {
            _icon.Texture = IconTexture;
            _icon.BasicTooltipText = _todo.IconTooltip;
        }

        private void OnNewMinute(object sender, GameTime e)
        {
            UpdateVisuals();
        }

        private Texture2D IconTexture => _todo.IconTooltip.IsNullOrEmpty()
            ? Resources.GetTexture(Textures.Empty) 
            : Resources.GetTexture(Textures.ScheduleIcon);

        protected override void DisposeControl()
        {
            _todo.Schedule.Reset.Unsubscribe(this);
            _todo.Schedule.Duration.Unsubscribe(this);
            _todo.Schedule.LocalTime.Unsubscribe(this);
            TimeService.NewMinute -= OnNewMinute;
            base.DisposeControl();
        }
    }
}