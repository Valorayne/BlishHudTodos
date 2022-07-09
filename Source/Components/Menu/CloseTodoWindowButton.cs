using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Utils;

namespace Todos.Source.Components.Menu
{
    public class CloseTodoWindowButton : Panel
    {
        public CloseTodoWindowButton()
        {
            Width = 40;
            Height = 40;
            
            new Image
            {
                Parent = this,
                Height = 36,
                Width = 36, 
                Location = new Point(2, 2),
                BasicTooltipText = "Minimize to menu bar",
                Texture = Resources.GetTexture(Textures.DeleteIcon)
            };
        }

        protected override void OnClick(MouseEventArgs e)
        {
            Settings.WindowShown.Value = false;
            base.OnClick(e);
        }
    }
}