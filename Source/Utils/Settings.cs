using Blish_HUD.Settings;
using Microsoft.Xna.Framework;

namespace TodoList
{
    public static class Settings
    {
        public static SettingEntry<bool> ShowMenuIcon { get; private set; }

        public static SettingEntry<int> OverlayWidth { get; private set; }
        public static SettingEntry<int> OverlayHeight { get; private set; }

        public static SettingEntry<float> OverlayBackgroundRed { get; private set; }
        public static SettingEntry<float> OverlayBackgroundBlue { get; private set; }
        public static SettingEntry<float> OverlayBackgroundGreen { get; private set; }
        public static SettingEntry<float> OverlayBackgroundAlpha { get; private set; }

        public static Color OverlayBackgroundColor => new Color(
            OverlayBackgroundRed.Value, OverlayBackgroundGreen.Value,
            OverlayBackgroundBlue.Value, OverlayBackgroundAlpha.Value
        );

        public static void Initialize(SettingCollection settings)
        {
            ShowMenuIcon = settings.DefineSetting("Menu.Icon.Show", true, () => "Show Menu Icon", () => "Whether or not to show the menu icon in the top left bar");
            
            var overlaySettings = settings.AddSubCollection("Overlay Settings", true, false);

            OverlayWidth = overlaySettings.DefineSetting("Width", 500, () => "Overlay Width",
                () => "An easier way to resize the window will be added in the future");
            OverlayWidth.SetRange(250, 1000);
            OverlayHeight = overlaySettings.DefineSetting("Height", 400, () => "Overlay Height",
                () => "An easier way to resize the window will be added in the future");
            OverlayHeight.SetRange(100, 1000);
            
            OverlayBackgroundAlpha = overlaySettings.DefineSetting("Background.Alpha", 0.2f, () => "Background Alpha");
            OverlayBackgroundAlpha.SetRange(0, 1f);
            OverlayBackgroundRed = overlaySettings.DefineSetting("Background.Red", 0f, () => "Background Red");
            OverlayBackgroundRed.SetRange(0, 1f);
            OverlayBackgroundBlue = overlaySettings.DefineSetting("Background.Blue", 0f, () => "Background Blue");
            OverlayBackgroundBlue.SetRange(0, 1f);
            OverlayBackgroundGreen = overlaySettings.DefineSetting("Background.Green", 0f, () => "Background Green");
            OverlayBackgroundGreen.SetRange(0, 1f);
        }

        public static void Dispose()
        {
            ShowMenuIcon = null;
            OverlayWidth = null;
            OverlayHeight = null;
            
            OverlayBackgroundRed = null;
            OverlayBackgroundGreen = null;
            OverlayBackgroundBlue = null;
            OverlayBackgroundAlpha = null;
        }
    }
}