using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using TodoList.Components.Details;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEditButton : Panel
    {
        public const int WIDTH = 45;
        private const int PADDING_RIGHT = 5;

        private readonly Todo _todo;
        private readonly StandardButton _editButton;

        public TodoEditButton(Todo todo)
        {
            _todo = todo;
            Height = HEADER_HEIGHT;
            Width = WIDTH;
            
            _editButton = new StandardButton
            {
                Parent = this,
                Text = "Edit",
                Width = WIDTH - PADDING_RIGHT,
                Location = new Point(0, 6)
            };
            _editButton.Click += OnButtonClicked;
        }
        
        private void OnButtonClicked(object target, MouseEventArgs args)
        {
            TodoDetailsWindowPool.Spawn(args.MousePosition, _todo);
        }

        protected override void DisposeControl()
        {
            _editButton.Click -= OnButtonClicked;
            _editButton.Dispose();
            base.DisposeControl();
        }
    }
}