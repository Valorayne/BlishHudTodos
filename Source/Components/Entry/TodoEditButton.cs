using Blish_HUD.Controls;
using TodoList.Components.Details;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEditButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;

        public TodoEditButton(Todo todo) : base(
            Resources.GetTexture(Textures.EditIcon),
            Resources.GetTexture(Textures.EditIconHovered),
            WIDTH, WIDTH,
            args => TodoDetailsWindowPool.Spawn(args.MousePosition, todo)
        )
        {
            BasicTooltipText = "Edit";
        }
    }
}