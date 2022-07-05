using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TodoList.Components.Menu
{
    public class TodoShowAlreadyDoneToggle : Image
    {
        public TodoShowAlreadyDoneToggle()
        {
            Texture = EyeTexture;
            BasicTooltipText = EyeTooltip;

            Click += OnClick;
        }

        private static Texture2D EyeTexture => Settings.ShowAlreadyDoneTasks.Value
            ? Resources.GetTexture(Textures.EyeIcon)
            : Resources.GetTexture(Textures.EyeIconClosed);

        private static string EyeTooltip => Settings.ShowAlreadyDoneTasks.Value
            ? "Hide already done tasks"
            : "Show already done tasks";

        private void OnClick(object sender, MouseEventArgs args)
        {
            Settings.ShowAlreadyDoneTasks.Value = !Settings.ShowAlreadyDoneTasks.Value;
            Texture = EyeTexture;
            BasicTooltipText = EyeTooltip;
        }

        protected override void DisposeControl()
        {
            Click -= OnClick;
            base.DisposeControl();
        }
    }
}