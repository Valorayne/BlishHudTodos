using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoWindow : ModuleEntity
    {
        private const int HORIZONTAL_PADDING = 5;
        private const int VERTICAL_PADDING = 5;
        
        private readonly Settings _settings;
        private readonly WindowBase2 _window;

        public Container Window => _window;

        public TodoWindow(Resources resources, Settings settings)
        {
            _settings = settings;
            _window = RegisterForDisposal(CreateWindow(resources));
            var scrollView = new TodoScrollView(_window, resources, settings).View;
            //var scrollBar = new TodoScrollBar(_window, scrollView);
        }

        private WindowBase2 CreateWindow(Resources resources)
        {
            var windowRec = new Rectangle(-20, 10, 500, 400);
            var contentRec = new Rectangle(
                windowRec.X + HORIZONTAL_PADDING, 
                windowRec.Y + VERTICAL_PADDING, 
                windowRec.Width - 2*HORIZONTAL_PADDING, 
                windowRec.Height - 2*VERTICAL_PADDING
            );
            var texture = resources.GetTexture(Textures.Empty);
            return new StandardWindow(texture, windowRec, contentRec)
            {
                Parent = GameService.Graphics.SpriteScreen,
                Title = "Todo List",
                BackgroundColor = GetBackgroundColor,
                SavesPosition = true,
                Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3"
            };
        }

        protected override void Initialize()
        {
            _settings.ShowMenuIcon.SettingChanged += OnShowMenuIconSettingChanged;
            
            _settings.OverlayBackgroundRed.SettingChanged += OnBackgroundColorsChanged;
            _settings.OverlayBackgroundGreen.SettingChanged += OnBackgroundColorsChanged;
            _settings.OverlayBackgroundBlue.SettingChanged += OnBackgroundColorsChanged;
            _settings.OverlayBackgroundAlpha.SettingChanged += OnBackgroundColorsChanged;
            
            _window.Show();
        }

        private Color GetBackgroundColor => new Color(
            _settings.OverlayBackgroundRed.Value, 
            _settings.OverlayBackgroundGreen.Value,
            _settings.OverlayBackgroundBlue.Value, 
            _settings.OverlayBackgroundAlpha.Value
        );

        private void OnShowMenuIconSettingChanged(object target, ValueChangedEventArgs<bool> args)
        {
            _window.CanClose = args.NewValue;
        }
        
        private void OnBackgroundColorsChanged(object target, ValueChangedEventArgs<float> args)
        {
            _window.BackgroundColor = GetBackgroundColor;
        }

        protected override void CustomDispose()
        {
            _settings.ShowMenuIcon.SettingChanged -= OnShowMenuIconSettingChanged;
            _settings.OverlayBackgroundRed.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundGreen.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundBlue.SettingChanged -= OnBackgroundColorsChanged;
            _settings.OverlayBackgroundAlpha.SettingChanged -= OnBackgroundColorsChanged;
        }
    }
}