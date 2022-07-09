using System;
using Blish_HUD.Controls;
using Todos.Source.Components.Generic;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Menu
{
    public sealed class TodoEditButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;

        public TodoEditButton(Action onEdit) : base(
            Resources.GetTexture(Textures.EditIcon),
            Resources.GetTexture(Textures.EditIconHovered),
            WIDTH, WIDTH,
            _ => onEdit())
        {
            IsEditing = false;
        }

        public bool IsEditing
        {
            set => BasicTooltipText = value ? "Stop Editing" : "Edit";
        }
    }
}