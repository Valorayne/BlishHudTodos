using System;
using Blish_HUD.Controls;

namespace TodoList.Components
{
    public sealed class TodoDeleteButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;

        public TodoDeleteButton(Action onDelete) : base(
            Resources.GetTexture(Textures.DeleteIcon),
            Resources.GetTexture(Textures.DeleteIconHovered),
            WIDTH, WIDTH,
            _ => onDelete()
        )
        {
            BasicTooltipText = "Delete";
        }
    }
}