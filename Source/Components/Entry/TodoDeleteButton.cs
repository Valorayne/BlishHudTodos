using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoDeleteButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;

        public TodoDeleteButton(Todo todo) : base(
            Resources.GetTexture(Textures.DeleteIcon),
            Resources.GetTexture(Textures.DeleteIconHovered),
            WIDTH, WIDTH,
            args => todo.Delete()
        )
        {
            BasicTooltipText = "Delete";
        }
    }
}