using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntry : Panel
    {
        private readonly BackgroundTextureSubscription _hoverSubscription;
        private readonly TodoEntryHoverMenu _hoverMenu;

        public TodoEntryContent EntryContent { get; }

        public TodoEntry(Todo todo)
        {
            WidthSizingMode = SizingMode.Fill;
            Height = HEADER_HEIGHT;

            EntryContent = new TodoEntryContent(todo) { Parent = this, Location = Point.Zero };
            _hoverMenu = new TodoEntryHoverMenu(todo) { Parent = this, Visible = false };
            
            _hoverSubscription = new BackgroundTextureSubscription(this, Resources.GetTexture(Textures.Header),
                Resources.GetTexture(Textures.HeaderHovered));
        }

        private void RepositionHoverMenu()
        {
            if (_hoverMenu != null)
                _hoverMenu.Location = new Point(Width - _hoverMenu.Width, 0);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            RepositionHoverMenu();
            base.OnResized(e);
        }

        protected override void OnMouseEntered(MouseEventArgs e)
        {
            _hoverMenu.Show();
            base.OnMouseEntered(e);
        }

        protected override void OnMouseLeft(MouseEventArgs e)
        {
            _hoverMenu.Hide();
            base.OnMouseLeft(e);
        }

        protected override void DisposeControl()
        {
            _hoverSubscription.Dispose();
            base.DisposeControl();
        }
    }
}