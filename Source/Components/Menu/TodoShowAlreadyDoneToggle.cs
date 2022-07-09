using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todos.Source.Utils;

namespace Todos.Source.Components.Menu
{
    public class TodoShowAlreadyDoneToggle : Panel
    {
        private readonly Image _icon;

        public TodoShowAlreadyDoneToggle()
        {
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

        private static Texture2D EyeTexture => Settings.ShowAlreadyDoneTasks.Value
            ? Resources.GetTexture(Textures.EyeIcon)
            : Resources.GetTexture(Textures.EyeIconClosed);

        private static string EyeTooltip => Settings.ShowAlreadyDoneTasks.Value
            ? "Hide already done tasks"
            : "Show already done tasks";

        protected override void OnClick(MouseEventArgs e)
        {
            Settings.ShowAlreadyDoneTasks.Value = !Settings.ShowAlreadyDoneTasks.Value;
            _icon.Texture = EyeTexture;
            _icon.BasicTooltipText = EyeTooltip;
            base.OnClick(e);
        }
    }
}