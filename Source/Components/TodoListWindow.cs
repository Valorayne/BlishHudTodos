using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoListWindow : StandardWindow
    {
        private const int MIN_WIDTH = 300;
        private const int MAX_WIDTH = 1000;
        private const int MIN_HEIGHT = 150;
        private const int MAX_HEIGHT = 1000;
        
        private readonly BackgroundColorSubscription _backgroundColorSubscription;

        private static Rectangle GetWindowRegion => new Rectangle(0, -28,
            Math.Max(MIN_WIDTH, Math.Min(MAX_WIDTH, Settings.OverlayWidth.Value)),
            Math.Max(MIN_HEIGHT, Math.Min(MAX_HEIGHT, Settings.OverlayHeight.Value)));
        private static Rectangle GetContentRegion => new Rectangle(0, -28, GetWindowRegion.Width, GetWindowRegion.Height + 33);

        public TodoListWindow() : base(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion)
        {
            Parent = GameService.Graphics.SpriteScreen;
            Title = "Todo List";
            BackgroundColor = Settings.OverlayBackgroundColor;
            SavesPosition = true;
            Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3";
            CanResize = true;
            CanClose = false;

            new TodoListPanel { Parent = this };
            _backgroundColorSubscription = new BackgroundColorSubscription(this);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            // hacky check to prevent infinite recursive call
            if (!Size.Equals(new Point(GetWindowRegion.Width, GetWindowRegion.Height + 40)))
                ConstructWindow(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion);

            Settings.OverlayWidth.Value = e.CurrentSize.X;
            Settings.OverlayHeight.Value = e.CurrentSize.Y; 
            
            base.OnResized(e);
        } 

        protected override void DisposeControl()
        {
            _backgroundColorSubscription.Dispose();
            base.DisposeControl();
        }
    }
}