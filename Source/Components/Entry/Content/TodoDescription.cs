using System;
using Blish_HUD.Controls;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Content
{
    public sealed class TodoDescription : Panel
    {
        private const int PADDING_RIGHT = 5;
        private readonly TodoEntryHoverMenu _hoverMenu;

        private readonly TextBox _input;
        private readonly TodoDescriptionLabel _label;

        public TodoDescription(TodoModel todo, TodoEntryHoverMenu hoverMenu)
        {
            _hoverMenu = hoverMenu;

            Height = HEADER_HEIGHT;
            WidthSizingMode = SizingMode.Fill;

            _label = new TodoDescriptionLabel(todo) { Parent = this };
            _input = new TodoDescriptionInput(todo) { Parent = this };

            hoverMenu.Shown += OnHoverMenuToggled;
            hoverMenu.Hidden += OnHoverMenuToggled;
        }

        private void OnHoverMenuToggled(object sender, EventArgs e)
        {
            ResizeLabel();
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            ResizeLabel();
            base.OnResized(e);
        }

        private void ResizeLabel()
        {
            if (_input != null)
                _input.Width = Width - _hoverMenu.Width - PADDING_RIGHT;
            if (_label != null)
                _label.Width = Width - (_hoverMenu.Visible ? _hoverMenu.Width : 0);
        }

        protected override void DisposeControl()
        {
            _hoverMenu.Shown -= OnHoverMenuToggled;
            _hoverMenu.Hidden -= OnHoverMenuToggled;
            base.DisposeControl();
        }
    }
}