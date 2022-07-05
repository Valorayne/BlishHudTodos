using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoListWindow : StandardWindow
    {
        private readonly BackgroundColorSubscription _backgroundColorSubscription;
        private readonly TodoListPanel _panel;

        private static Rectangle GetWindowRegion => new Rectangle(0, -28, Settings.OverlayWidth.Value, Settings.OverlayHeight.Value);
        private static Rectangle GetContentRegion => new Rectangle(0, -28, GetWindowRegion.Width, GetWindowRegion.Height + 33);

        public TodoListWindow() : base(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion)
        {
            Parent = GameService.Graphics.SpriteScreen;
            Title = "Todo List";
            BackgroundColor = Settings.OverlayBackgroundColor;
            SavesPosition = true;
            Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3";
            CanClose = false;
            CanResize = true;
            Padding = Thickness.Zero;

            _panel = new TodoListPanel { Parent = this };
            _backgroundColorSubscription = new BackgroundColorSubscription(this);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (!Size.Equals(new Point(GetWindowRegion.Width, GetWindowRegion.Height + 40)))
                ConstructWindow(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion);
            
            Settings.OverlayWidth.Value = e.CurrentSize.X;
            Settings.OverlayHeight.Value = e.CurrentSize.Y; 
            
            base.OnResized(e);
        } 

        protected override void DisposeControl()
        {
            _panel.Dispose();
            _backgroundColorSubscription.Dispose();
            base.DisposeControl();
        }
    }
}