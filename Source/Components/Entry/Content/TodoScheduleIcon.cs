using Blish_HUD.Controls;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xna.Framework;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoScheduleIcon : Panel
    {
        public const int WIDTH = HEADER_HEIGHT;
        
        private readonly TodoScheduleModel _schedule;

        public TodoScheduleIcon(TodoScheduleModel schedule)
        {
            _schedule = schedule;
            
            Width = WIDTH;
            Height = WIDTH;
            
            var icon = new Image(Resources.GetTexture(Textures.Empty))
            {
                Parent = this,
                Location = new Point(0, 2),
                Width = 32,
                Height = 32,
            };

            schedule.IconTooltip.Subscribe(this, tooltip =>
            {
                icon.Texture = Resources.GetTexture(tooltip.IsNullOrEmpty() ? Textures.Empty : Textures.ScheduleIcon);
                icon.BasicTooltipText = tooltip;
            });
        }

        protected override void DisposeControl()
        {
            _schedule.IconTooltip.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}