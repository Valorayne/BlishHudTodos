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

        public TodoWindow(Resources resources, Settings settings) : base(
            resources.GetTexture(Textures.Empty), 
            new Rectangle(
                WINDOW_X, 
                WINDOW_Y, 
                settings.OverlayWidth.Value, 
                settings.OverlayHeight.Value
            ),
            new Rectangle(
                WINDOW_X + HORIZONTAL_PADDING, 
                WINDOW_Y + VERTICAL_PADDING, 
                settings.OverlayWidth.Value - 2*HORIZONTAL_PADDING, 
                settings.OverlayHeight.Value - 2*VERTICAL_PADDING
            )
        )
        {
            _settings = settings;
            
            Parent = GameService.Graphics.SpriteScreen;
            Title = "Todo List";
            BackgroundColor = GetBackgroundColor;
            SavesPosition = true;
            Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3";
            CanClose = false;
            
            var scrollView = new TodoScrollView(this, resources, settings).View;
            //var scrollBar = new TodoScrollBar(_window, scrollView);
            
            _settings.OverlayBackgroundRed.SettingChanged += OnBackgroundColorsChanged;
            _settings.OverlayBackgroundGreen.SettingChanged += OnBackgroundColorsChanged;
            _settings.OverlayBackgroundBlue.SettingChanged += OnBackgroundColorsChanged;
            _settings.OverlayBackgroundAlpha.SettingChanged += OnBackgroundColorsChanged;
        }

        private Color GetBackgroundColor => new Color(
            _settings.OverlayBackgroundRed.Value, 
            _settings.OverlayBackgroundGreen.Value,
            _settings.OverlayBackgroundBlue.Value, 
            _settings.OverlayBackgroundAlpha.Value
        );
        
        private void OnBackgroundColorsChanged(object target, ValueChangedEventArgs<float> args)
        {
            BackgroundColor = GetBackgroundColor;
        }

        protected override void DisposeControl()
        {
            _settings.OverlayBackgroundRed.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundGreen.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundBlue.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundAlpha.SettingChanged -= OnBackgroundColorsChanged;
            
            base.DisposeControl();;
        }
    }
}