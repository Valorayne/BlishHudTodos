using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Components.Menu;

namespace TodoList.Components
{
    public class TodoListWindow : StandardWindow
    {
        private const int WINDOW_X = 20;
        private const int WINDOW_Y = 40;
        private const int HORIZONTAL_PADDING = 5;
        private const int VERTICAL_PADDING = 5;
        
        private const int MENU_BAR_HEIGHT = 30;
        private const int MENU_BAR_PADDING = 5;
        
        private readonly TodoScrollView _scrollView;
        private readonly Scrollbar _scrollBar;
        private readonly AddNewTodoButton _addNewButton;
        private readonly BackgroundColorSubscription _backgroundColorSubscription;
        private readonly Image _eyeButton;
        
        private static Rectangle GetWindowRegion => new Rectangle(WINDOW_X, WINDOW_Y, Settings.OverlayWidth.Value, Settings.OverlayHeight.Value);

        private static Rectangle GetContentRegion => new Rectangle(
            WINDOW_X + HORIZONTAL_PADDING, 
            WINDOW_Y + VERTICAL_PADDING, 
            Settings.OverlayWidth.Value - 2 * HORIZONTAL_PADDING, 
            Settings.OverlayHeight.Value - 2 * VERTICAL_PADDING
        );

        public TodoListWindow() : base(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion)
        {
            Parent = GameService.Graphics.SpriteScreen;
            Title = "Todo List";
            BackgroundColor = Settings.OverlayBackgroundColor;
            SavesPosition = true;
            Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3";
            CanClose = false;
            CanResize = true;

            var scrollHeight = ContentRegion.Height - MENU_BAR_HEIGHT;
            _scrollView = new TodoScrollView { Parent = this, Height = scrollHeight };
            _scrollBar = AddScrollBar(ContentRegion, scrollHeight);
            var buttonLocation = new Point(WindowRegion.Width - 140, scrollHeight + MENU_BAR_PADDING);
            _addNewButton = new AddNewTodoButton { Parent = this, Location = buttonLocation };
            _eyeButton = new TodoShowAlreadyDoneToggle
            {
                Parent = this, 
                Location = new Point(buttonLocation.X-32, buttonLocation.Y),
                Width = 32,
                Height = 32
            };

            _backgroundColorSubscription = new BackgroundColorSubscription(this);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (!Size.Equals(new Point(GetWindowRegion.Width, GetWindowRegion.Height + 40)))
                ConstructWindow(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion);
            
            Settings.OverlayWidth.Value = e.CurrentSize.X;
            Settings.OverlayHeight.Value = e.CurrentSize.Y; 
            
            var scrollHeight = ContentRegion.Height - MENU_BAR_HEIGHT;
            if (_scrollView != null) _scrollView.Height = scrollHeight;
            if (_scrollBar != null) _scrollBar.Height = scrollHeight;
            if (_scrollBar != null) _scrollBar.Location = new Point(ContentRegion.Width - 10, 0);
            var buttonLocation = new Point(WindowRegion.Width - 140, scrollHeight + MENU_BAR_PADDING);
            if (_addNewButton != null) _addNewButton.Location = buttonLocation;
            if (_eyeButton != null) _eyeButton.Location = new Point(buttonLocation.X - 32, buttonLocation.Y);
            
            base.OnResized(e);
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