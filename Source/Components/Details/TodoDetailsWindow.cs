using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsWindow : StandardWindow
    {
        public const int WIDTH = 400;
        private const int HEIGHT = 200;
        private const int WINDOW_X = -20;
        private const int WINDOW_Y = 10;
        private const int HORIZONTAL_PADDING = 5;
        private const int VERTICAL_PADDING = 5;
        
        public static TodoDetailsWindow Create(Point location, Todo existingTodo = null)
        {
            var hp = HORIZONTAL_PADDING;
            var vp = VERTICAL_PADDING;
            var windowRegion = new Rectangle(WINDOW_X, WINDOW_Y, WIDTH, HEIGHT);
            var contentRegion = new Rectangle(WINDOW_X + hp, WINDOW_Y + vp, WIDTH - 2 * hp, HEIGHT - 2 * vp);
            return new TodoDetailsWindow(windowRegion, contentRegion, location, existingTodo);
        }

        private readonly Subscriptions.BackgroundColor _backgroundColorSubscription;

        private TodoDetailsWindow(Rectangle windowRegion, Rectangle contentRegion, Point location,
            Todo existingTodo = null) : base(Resources.GetTexture(Textures.Empty), windowRegion, contentRegion)
        {
            Parent = GameService.Graphics.SpriteScreen;
            CanClose = true;
            BackgroundColor = Settings.OverlayBackgroundColor;
            //SavesPosition = true;
            //Id = "e5b47462-7eb9-4a91-b733-da220eccbf98";
            Title = existingTodo != null ? "Edit Todo" : "Add New Todo";
            Location = location;

            _backgroundColorSubscription = new Subscriptions.BackgroundColor(this);
        }

        protected override void DisposeControl()
        {
            _backgroundColorSubscription.Dispose();
            base.DisposeControl();
        }
    }
}