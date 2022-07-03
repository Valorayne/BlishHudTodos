using Blish_HUD.Content;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public sealed class TodoEditButton : Panel
    {
        public const int WIDTH = 45;
        private const int PADDING_RIGHT = 5;

        private readonly StandardButton _editButton;

        public TodoEditButton()
        {
            Height = HEADER_HEIGHT;
            Width = WIDTH;
            
            _editButton = new StandardButton
            {
                Parent = this,
                Text = "Edit",
                Width = WIDTH - PADDING_RIGHT,
                Location = new Point(0, 6)
            };
        }

        public bool Expanded { get; set; }

        protected override void DisposeControl()
        {
            _editButton.Dispose();
            base.DisposeControl();
        }
    }
}