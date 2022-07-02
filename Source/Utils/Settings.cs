using Blish_HUD.Settings;

namespace TodoList
{
    public class Settings
    {
        public SettingEntry<bool> ShowMenuIcon { get; }
        
        public SettingEntry<float> OverlayBackgroundRed { get; }
        public SettingEntry<float> OverlayBackgroundBlue { get; }
        public SettingEntry<float> OverlayBackgroundGreen { get; }
        public SettingEntry<float> OverlayBackgroundAlpha { get; }

        public Settings(SettingCollection settings)
        {
            ShowMenuIcon = settings.DefineSetting("Menu.Icon.Show", true, () => "Show Menu Icon", () => "Whether or not the menu icon in the top left bar should be shown");

            var overlaySettings = settings.AddSubCollection("Overlay Settings", true, false);
            OverlayBackgroundAlpha = overlaySettings.DefineSetting("Background.Alpha", 0.2f, () => "Background Alpha");
            OverlayBackgroundAlpha.SetRange(0, 1f);
            OverlayBackgroundRed = overlaySettings.DefineSetting("Background.Red", 0f, () => "Background Red");
            OverlayBackgroundRed.SetRange(0, 1f);
            OverlayBackgroundBlue = overlaySettings.DefineSetting("Background.Blue", 0f, () => "Background Blue");
            OverlayBackgroundBlue.SetRange(0, 1f);
            OverlayBackgroundGreen = overlaySettings.DefineSetting("Background.Green", 0f, () => "Background Green");
            OverlayBackgroundGreen.SetRange(0, 1f);
        }
    }
}