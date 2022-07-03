using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using TodoList.Components.Details;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEditButton : Panel
    {
        public const int WIDTH = HEADER_HEIGHT;
        private const int PADDING_RIGHT = 5;

        private readonly HoverButton _editButton;

        public TodoEditButton(Todo todo)
        {
            Height = WIDTH;
            Width = WIDTH;
            
            _editButton = new HoverButton(
                Resources.GetTexture(Textures.EditIcon), 
                Resources.GetTexture(Textures.EditIconHovered), 
                WIDTH, WIDTH, 
                args => TodoDetailsWindowPool.Spawn(args.MousePosition, todo))
            {
                Parent = this,
                Location = new Point(0, 2),
            };
        }

        protected override void DisposeControl()
        {
            _editButton.Dispose();
            base.DisposeControl();
        }
    }
}