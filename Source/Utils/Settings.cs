using Blish_HUD.Input;
using Blish_HUD.Settings;

namespace Todos.Source.Utils
{
    public static class Settings
    {
        public static SettingEntry<bool> WindowMinimized { get; private set; }
        public static SettingEntry<bool> AlwaysShowWindow { get; private set; }
        public static SettingEntry<bool> ShowWindowOnMap { get; private set; }
        public static SettingEntry<bool> FixatedWindow { get; private set; }
        
        public static SettingEntry<float> BackgroundOpacity { get; private set; }
        public static SettingEntry<float> WindowOpacityWhenNotFocussed { get; private set; }

        public static SettingEntry<int> WindowLocationX { get; private set; }
        public static SettingEntry<int> WindowLocationY { get; private set; }
        public static SettingEntry<int> WindowWidth { get; private set; }
        public static SettingEntry<int> WindowHeight { get; private set; }
        
        public static SettingEntry<bool> ShowAlreadyDoneTasks { get; private set; }
        
        public static SettingEntry<KeyBinding> ToggleWindowHotkey { get; private set; }
        public static SettingEntry<CheckboxType> CheckboxType { get; private set; }

        public static void Initialize(SettingCollection settings)
        {
            WindowMinimized = settings.DefineSetting("Window.Visibility.Minimized", false);
            AlwaysShowWindow = settings.DefineSetting("Window.Visibility.Always", false);
            ShowWindowOnMap = settings.DefineSetting("Window.Visibility.OnMap", false);
            FixatedWindow = settings.DefineSetting("Window.Location.Fixed", false);

            BackgroundOpacity = settings.DefineSetting("Window.Background.Opacity", 0.2f);
            WindowOpacityWhenNotFocussed = settings.DefineSetting("Window.Visibility.Opacity.WhenNotFocussed", 1f);

            WindowLocationX = settings.DefineSetting("Window.Location.X", 200);
            WindowLocationY = settings.DefineSetting("Window.Location.Y", 200);
            WindowWidth = settings.DefineSetting("Window.Dimensions.Width", 400);
            WindowHeight = settings.DefineSetting("Window.Dimensions.Height", 200);

            ShowAlreadyDoneTasks = settings.DefineSetting("Menu.Bar.ShowAlreadyDoneTasks", true);

            ToggleWindowHotkey = settings.DefineSetting("Hotkeys.Window.Toggle", new KeyBinding());
            CheckboxType = settings.DefineSetting("Checkbox.Type", Utils.CheckboxType.Standard);
        }

        public static void Dispose()
        {
            WindowMinimized = null;
            AlwaysShowWindow = null;
            ShowWindowOnMap = null;
            FixatedWindow = null;

            BackgroundOpacity = null;
            WindowOpacityWhenNotFocussed = null;

            WindowLocationX = null;
            WindowLocationY = null;
            WindowWidth = null;
            WindowHeight = null;

            ShowAlreadyDoneTasks = null;

            ToggleWindowHotkey = null;
            CheckboxType = null;
        }
    }
}