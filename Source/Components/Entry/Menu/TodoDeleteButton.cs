using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public sealed class TodoDeleteButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;

        public TodoDeleteButton(Action<Point> onDelete) : base(
            Resources.GetTexture(Textures.DeleteIcon),
            Resources.GetTexture(Textures.DeleteIconHovered),
            WIDTH, WIDTH,
            args => onDelete(args.MousePosition)
        )
        {
            BasicTooltipText = "Delete";
        }
    }
}