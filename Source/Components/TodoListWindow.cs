using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Components.Menu;

namespace TodoList.Components
{
    public class TodoListWindow : StandardWindow
    {
        private const int WINDOW_X = -20;
        private const int WINDOW_Y = 10;
        private const int HORIZONTAL_PADDING = 5;
        private const int VERTICAL_PADDING = 5;
        
        private readonly TodoScrollView _scrollView;
        private readonly TodoScrollBar _scrollBar;
        private readonly AddNewTodoButton _addNewButton;
        private readonly Subscriptions.BackgroundColor _backgroundColorSubscription;

        public static TodoListWindow Create()
        {
            var hp = HORIZONTAL_PADDING;
            var vp = VERTICAL_PADDING;
            var width = Settings.OverlayWidth.Value;
            var height = Settings.OverlayHeight.Value;
            var windowRegion = new Rectangle(WINDOW_X, WINDOW_Y, width, height);
            var contentRegion = new Rectangle(WINDOW_X + hp, WINDOW_Y + vp, width - 2 * hp, height - 2 * vp);
            return new TodoListWindow(windowRegion, contentRegion);
        }

        private TodoListWindow(Rectangle windowRegion, Rectangle contentRegion) 
                : base(Resources.GetTexture(Textures.Empty), windowRegion, contentRegion)
        {
            Parent = GameService.Graphics.SpriteScreen;
            Title = "Todo List";
            BackgroundColor = Settings.OverlayBackgroundColor;
            SavesPosition = true;
            Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3";
            CanClose = false;
            
            _scrollView = new TodoScrollView { Parent = this };
            _scrollBar = new TodoScrollBar(_scrollView, contentRegion.Width, contentRegion.Height) { Parent = this };
            _addNewButton = new AddNewTodoButton { Parent = this };

            _backgroundColorSubscription = new Subscriptions.BackgroundColor(this);
        }

        protected override void DisposeControl()
        {
            _scrollBar.Dispose();
            _scrollView.Dispose();
            _addNewButton.Dispose();
            _backgroundColorSubscription.Dispose();
            base.DisposeControl();
        }
    }
}