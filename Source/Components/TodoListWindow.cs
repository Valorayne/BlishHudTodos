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
        
        private const int MENU_BAR_HEIGHT = 30;
        private const int MENU_BAR_PADDING = 5;
        
        private readonly TodoScrollView _scrollView;
        private readonly Scrollbar _scrollBar;
        private readonly AddNewTodoButton _addNewButton;
        private readonly BackgroundColorSubscription _backgroundColorSubscription;
        private readonly Image _eyeButton;

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

            var scrollHeight = contentRegion.Height - MENU_BAR_HEIGHT;
            _scrollView = new TodoScrollView { Parent = this, Height = scrollHeight };
            _scrollBar = AddScrollBar(contentRegion, scrollHeight);
            var buttonLocation = new Point(windowRegion.Width - 140, scrollHeight + MENU_BAR_PADDING);
            _addNewButton = new AddNewTodoButton { Parent = this, Location = buttonLocation };
            _eyeButton = new Image(Resources.GetTexture(Textures.EyeIcon))
            {
                Parent = this,
                Location = new Point(buttonLocation.X-200, buttonLocation.Y)
            };

            _backgroundColorSubscription = new BackgroundColorSubscription(this);
        }

        private Scrollbar AddScrollBar(Rectangle contentRegion, int scrollHeight)
        {
            return new Scrollbar(_scrollView)
            {
                Parent = this,
                Height = scrollHeight,
                Location = new Point(contentRegion.Width - 10, 0)
            };
        }

        protected override void DisposeControl()
        {
            _eyeButton.Dispose();
            _scrollBar.Dispose();
            _scrollView.Dispose();
            _addNewButton.Dispose();
            _backgroundColorSubscription.Dispose();
            base.DisposeControl();
        }
    }
}