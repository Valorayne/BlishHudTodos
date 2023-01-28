using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todos.Source.Models;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Menu
{
    public class LockAllTasksToggle : Panel
    {
        private readonly SettingsModel _settings;
        private readonly Image _icon;

        public LockAllTasksToggle(SettingsModel settings)
        {
            _settings = settings;
            _icon = new Image
            {
                Parent = this,
                Height = 34,
                Width = 34, 
                Location = new Point(3, 3),
                BasicTooltipText = LockTooltip,
                Texture = LockTexture
            };
            
            Height = 40;
            Width = 40;
        }

        private Texture2D LockTexture => _settings.LockAllTasks.Value
            ? Resources.GetTexture(Textures.LockIconActive)
            : Resources.GetTexture(Textures.LockIcon);

        private string LockTooltip => _settings.LockAllTasks.Value
            ? "Enable editing of tasks"
            : "Disable editing of tasks";

        protected override void OnClick(MouseEventArgs e)
        {
            _settings.LockAllTasks.Toggle();
            _icon.Texture = LockTexture;
            _icon.BasicTooltipText = LockTooltip;
            base.OnClick(e);
        }
    }
}