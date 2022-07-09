using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Utils;

namespace Todos.Source.Components.Menu
{
    public class CloseTodoWindowButton : Panel
    {
        public CloseTodoWindowButton()
        {
            Width = 40;
            Height = 40;
            
            new HoverButton(Resources.GetTexture(Textures.CloseIcon), Resources.GetTexture(Textures.CloseIconHovered), 
                26, 26, _ => Settings.WindowShown.Value = false)
            {
                Parent = this,
                Location = new Point(7, 7),
                BasicTooltipText = "Minimize to menu bar",
            };
        }
    }
}