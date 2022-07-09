using System;
using Blish_HUD.Controls;

namespace TodoList.Components
{
    public sealed class TodoEditButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;

        public TodoEditButton(Action onEdit) : base(
            Resources.GetTexture(Textures.EditIcon),
            Resources.GetTexture(Textures.EditIconHovered),
            WIDTH, WIDTH,
            _ =>
            {
                onEdit();
            })
        {
            IsEditing = false;
        }

        public bool IsEditing
        {
            set => BasicTooltipText = value ? "Stop Editing" : "Edit";
        }
    }
}