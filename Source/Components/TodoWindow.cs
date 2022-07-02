using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoWindow : ModuleEntity
    {
        private readonly Settings _settings;
        private readonly TabbedWindow2 _window;

        public Container Window => _window;

        public TodoWindow(Resources resources, Settings settings)
        {
            _settings = settings;
            _window = RegisterForDisposal(CreateWindow(resources));
        }

        private TabbedWindow2 CreateWindow(Resources resources)
        {
            var windowRec = new Rectangle(-20, 20, 500, 400);
            var contentRec = new Rectangle(windowRec.X + 47, windowRec.Y + 5, windowRec.Width - 5, windowRec.Height - 10);
            var texture = resources.GetTexture(Textures.Empty);
            return new TabbedWindow2(texture, windowRec, contentRec)
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