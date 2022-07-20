using Blish_HUD.Controls;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Content
{
    public sealed class TodoDescription : Panel
    {
        private const int PADDING_RIGHT = 5;

        private readonly TodoEntryHoverMenu _hoverMenu;
        private readonly TodoDescriptionLabel _label;

        private readonly TextBox _input;

        public TodoDescription(TodoModel todo, TodoEntryHoverMenu hoverMenu)
        {
            _hoverMenu = hoverMenu;
            
            Height = HEADER_HEIGHT;

            _label = new TodoDescriptionLabel(todo) { Parent = this };
            _input = new TodoDescriptionInput(todo) { Parent = this };
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (_input != null)
                _input.Width = Width - _hoverMenu.Width - PADDING_RIGHT;
            if (_label != null)
                _label.Width = Width - _hoverMenu.Width;
            base.OnResized(e);
        }
    }
}