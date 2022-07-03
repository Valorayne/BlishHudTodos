using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoWindow : StandardWindow
    {
        private const int WINDOW_X = -20;
        private const int WINDOW_Y = 10;
        private const int HORIZONTAL_PADDING = 5;
        private const int VERTICAL_PADDING = 5;
        
        private readonly Settings _settings;
        private readonly TodoScrollView _scrollView;
        private readonly TodoScrollBar _scrollBar;

        public static TodoWindow Create(Settings settings)
        {
            var hp = HORIZONTAL_PADDING;
            var vp = VERTICAL_PADDING;
            var width = settings.OverlayWidth.Value;
            var height = settings.OverlayHeight.Value;
            var windowRegion = new Rectangle(WINDOW_X, WINDOW_Y, width, height);
            var contentRegion = new Rectangle(WINDOW_X + hp, WINDOW_Y + vp, width - 2 * hp, height - 2 * vp);
            return new TodoWindow(windowRegion, contentRegion, settings);
        }

        private TodoWindow(Rectangle windowRegion, Rectangle contentRegion, Settings settings) 
                : base(Resources.GetTexture(Textures.Empty), windowRegion, contentRegion)
        {
            _settings = settings;
            
            Parent = GameService.Graphics.SpriteScreen;
            Title = "Todo List";
            BackgroundColor = settings.OverlayBackgroundColor;
            SavesPosition = true;
            Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3";
            CanClose = false;
            
            settings.OverlayBackgroundRed.SettingChanged += OnBackgroundColorsChanged;
            settings.OverlayBackgroundGreen.SettingChanged += OnBackgroundColorsChanged;
            settings.OverlayBackgroundBlue.SettingChanged += OnBackgroundColorsChanged;
            settings.OverlayBackgroundAlpha.SettingChanged += OnBackgroundColorsChanged;
            
            _scrollView = new TodoScrollView(settings) { Parent = this };
            _scrollBar = new TodoScrollBar(_scrollView, contentRegion.Width, contentRegion.Height) { Parent = this };
        }
        
        private void OnBackgroundColorsChanged(object target, ValueChangedEventArgs<float> args)
        {
            BackgroundColor = _settings.OverlayBackgroundColor;
        }

        protected override void DisposeControl()
        {
            _scrollBar.Dispose();
            _scrollView.Dispose();
            
            _settings.OverlayBackgroundRed.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundGreen.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundBlue.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundAlpha.SettingChanged -= OnBackgroundColorsChanged;
            
            base.DisposeControl();
        }
    }
}