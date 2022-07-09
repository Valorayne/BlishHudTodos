using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Menu
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