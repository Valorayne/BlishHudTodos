using Blish_HUD.Settings;

namespace TodoList
{
    public class Settings
    {
        public SettingEntry<bool> ShowMenuIcon { get; }
        
        public SettingEntry<int> OverlayWidth { get; }
        public SettingEntry<int> OverlayHeight { get; }

        public SettingEntry<float> OverlayBackgroundRed { get; }
        public SettingEntry<float> OverlayBackgroundBlue { get; }
        public SettingEntry<float> OverlayBackgroundGreen { get; }
        public SettingEntry<float> OverlayBackgroundAlpha { get; }

        public Settings(SettingCollection settings)
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
    }
}