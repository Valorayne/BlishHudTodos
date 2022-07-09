using Blish_HUD.Controls;
using Blish_HUD.Input;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public sealed class TodoCornerIcon : CornerIcon
    {
        public TodoCornerIcon()
        {
            Icon = Resources.GetTexture(Textures.CornerIcon);
            BasicTooltipText = "Todos";
        }

        protected override void OnClick(MouseEventArgs e)
        {
            Settings.WindowMinimized.Value = false;
            base.OnClick(e);
        }
    }
}