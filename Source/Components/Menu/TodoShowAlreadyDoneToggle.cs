using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todos.Source.Utils;

namespace Todos.Source.Components.Menu
{
    public class TodoShowAlreadyDoneToggle : Panel
    {
        private readonly SettingsModel _settings;
        private readonly Image _icon;

        public TodoShowAlreadyDoneToggle(SettingsModel settings)
        {
            _settings = settings;
            _icon = new Image
            {
                Parent = this,
                Height = 36,
                Width = 36, 
                Location = new Point(2, 2),
                BasicTooltipText = EyeTooltip,
                Texture = EyeTexture
            };
            
            Height = 40;
            Width = 40;
        }

        private Texture2D EyeTexture => _settings.ShowAlreadyDoneTasks.Value
            ? Resources.GetTexture(Textures.EyeIcon)
            : Resources.GetTexture(Textures.EyeIconClosed);

        private string EyeTooltip => _settings.ShowAlreadyDoneTasks.Value
            ? "Hide already done tasks"
            : "Show already done tasks";

        protected override void OnClick(MouseEventArgs e)
        {
            _settings.ShowAlreadyDoneTasks.Value = !_settings.ShowAlreadyDoneTasks.Value;
            _icon.Texture = EyeTexture;
            _icon.BasicTooltipText = EyeTooltip;
            base.OnClick(e);
        }
    }
}