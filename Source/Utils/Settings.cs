using Blish_HUD.Settings;

namespace Todos.Source.Utils
{
    public static class Settings
    {
        public static SettingEntry<bool> WindowMinimized { get; private set; }
        public static SettingEntry<bool> ShowWindowOnMap { get; private set; }
        
        public static SettingEntry<float> WindowOpacityWhenNotFocussed { get; private set; }

        public static SettingEntry<int> WindowWidth { get; private set; }
        public static SettingEntry<int> WindowHeight { get; private set; }
        
        public static SettingEntry<string> Data { get; private set; }
        
        public static SettingEntry<bool> ShowAlreadyDoneTasks { get; private set; }

        public static void Initialize(SettingCollection settings)
        {
            WindowMinimized = settings.DefineSetting("Window.Visibility.Minimized", false);
            ShowWindowOnMap = settings.DefineSetting("Window.Visibility.OnMap", false);

            WindowOpacityWhenNotFocussed = settings.DefineSetting("Window.Visibility.Opacity.WhenNotFocussed", 1f);

            WindowWidth = settings.DefineSetting("Window.Dimensions.Width", 400);
            WindowHeight = settings.DefineSetting("Window.Dimensions.Height", 200);

            Data = settings.DefineSetting("Data.Todos.1", "{}");

            ShowAlreadyDoneTasks = settings.DefineSetting("Menu.Bar.ShowAlreadyDoneTasks", true);
        }

        public static void Dispose()
        {
            WindowMinimized = null;
            ShowWindowOnMap = null;
            
            WindowWidth = null;
            WindowHeight = null;

            Data = null;

            ShowAlreadyDoneTasks = null;
        }
    }
}