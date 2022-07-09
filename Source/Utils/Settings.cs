using Blish_HUD.Settings;
using Microsoft.Xna.Framework;

namespace Todos.Source.Utils
{
    public static class Settings
    {
        public static SettingEntry<bool> WindowMinimized { get; private set; }
        public static SettingEntry<bool> ShowWindowOnMap { get; private set; }

        public static SettingEntry<int> OverlayWidth { get; private set; }
        public static SettingEntry<int> OverlayHeight { get; private set; }

        public static SettingEntry<float> OverlayBackgroundRed { get; private set; }
        public static SettingEntry<float> OverlayBackgroundBlue { get; private set; }
        public static SettingEntry<float> OverlayBackgroundGreen { get; private set; }
        public static SettingEntry<float> OverlayBackgroundAlpha { get; private set; }
        
        public static SettingEntry<string> Data { get; private set; }
        
        public static SettingEntry<bool> ShowAlreadyDoneTasks { get; private set; }

        public static Color OverlayBackgroundColor => new Color(
            OverlayBackgroundRed.Value, OverlayBackgroundGreen.Value,
            OverlayBackgroundBlue.Value, OverlayBackgroundAlpha.Value
        );

        public static void Initialize(SettingCollection settings)
        {
            WindowMinimized = settings.DefineSetting("Window.Visibility.Minimized", false);
            ShowWindowOnMap = settings.DefineSetting("Window.Visibility.OnMap", false);
            
            var overlaySettings = settings.AddSubCollection("Overlay Settings", true, false);

            OverlayWidth = overlaySettings.DefineSetting("Width", 500);
            OverlayHeight = overlaySettings.DefineSetting("Height", 400);
            
            OverlayBackgroundAlpha = overlaySettings.DefineSetting("Background.Alpha", 0.2f, () => "Background Alpha");
            OverlayBackgroundAlpha.SetRange(0, 1f);
            OverlayBackgroundRed = overlaySettings.DefineSetting("Background.Red", 0f, () => "Background Red");
            OverlayBackgroundRed.SetRange(0, 1f);
            OverlayBackgroundBlue = overlaySettings.DefineSetting("Background.Blue", 0f, () => "Background Blue");
            OverlayBackgroundBlue.SetRange(0, 1f);
            OverlayBackgroundGreen = overlaySettings.DefineSetting("Background.Green", 0f, () => "Background Green");
            OverlayBackgroundGreen.SetRange(0, 1f);

            Data = settings.DefineSetting("Data.Todos.1", "{}");

            ShowAlreadyDoneTasks = settings.DefineSetting("Menu.Bar.ShowAlreadyDoneTasks", true);
        }

        public static void Dispose()
        {
            WindowMinimized = null;
            ShowWindowOnMap = null;
            
            OverlayWidth = null;
            OverlayHeight = null;
            
            OverlayBackgroundRed = null;
            OverlayBackgroundGreen = null;
            OverlayBackgroundBlue = null;
            OverlayBackgroundAlpha = null;

            Data = null;

            ShowAlreadyDoneTasks = null;
        }
    }
}