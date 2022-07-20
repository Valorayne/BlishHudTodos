using Blish_HUD.Input;
using Blish_HUD.Settings;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Utils
{
    public static class Settings
    {
        public static IVariable<bool> WindowMinimized { get; private set; }
        public static IVariable<bool> AlwaysShowWindow { get; private set; }
        public static IVariable<bool> ShowWindowOnMap { get; private set; }
        public static IVariable<bool> FixatedWindow { get; private set; }
        
        public static IVariable<float> BackgroundOpacity { get; private set; }
        public static IVariable<float> WindowOpacityWhenNotFocussed { get; private set; }

        public static IVariable<int> WindowLocationX { get; private set; }
        public static IVariable<int> WindowLocationY { get; private set; }
        public static IVariable<int> WindowWidth { get; private set; }
        public static IVariable<int> WindowHeight { get; private set; }
        
        public static IVariable<bool> ShowAlreadyDoneTasks { get; private set; }
        
        public static IVariable<KeyBinding> ToggleWindowHotkey { get; private set; }
        public static IVariable<CheckboxType> CheckboxType { get; private set; }

        public static void Initialize(SettingCollection settings)
        {
            WindowMinimized = settings.DefineSetting("Window.Visibility.Minimized", false).ToVariable();
            AlwaysShowWindow = settings.DefineSetting("Window.Visibility.Always", false).ToVariable();
            ShowWindowOnMap = settings.DefineSetting("Window.Visibility.OnMap", false).ToVariable();
            FixatedWindow = settings.DefineSetting("Window.Location.Fixed", false).ToVariable();

            BackgroundOpacity = settings.DefineSetting("Window.Background.Opacity", 0.2f).ToVariable();
            WindowOpacityWhenNotFocussed = settings.DefineSetting("Window.Visibility.Opacity.WhenNotFocussed", 1f).ToVariable();

            WindowLocationX = settings.DefineSetting("Window.Location.X", 200).ToVariable();
            WindowLocationY = settings.DefineSetting("Window.Location.Y", 200).ToVariable();
            WindowWidth = settings.DefineSetting("Window.Dimensions.Width", 400).ToVariable();
            WindowHeight = settings.DefineSetting("Window.Dimensions.Height", 200).ToVariable();

            ShowAlreadyDoneTasks = settings.DefineSetting("Menu.Bar.ShowAlreadyDoneTasks", true).ToVariable();

            ToggleWindowHotkey = settings.DefineSetting("Hotkeys.Window.Toggle", new KeyBinding()).ToVariable();
            CheckboxType = settings.DefineSetting("Checkbox.Type", Utils.CheckboxType.Standard).ToVariable();
        }

        public static void Dispose()
        {
            WindowMinimized.Dispose();
            AlwaysShowWindow.Dispose();
            ShowWindowOnMap.Dispose();
            FixatedWindow.Dispose();

            BackgroundOpacity.Dispose();
            WindowOpacityWhenNotFocussed.Dispose();

            WindowLocationX.Dispose();
            WindowLocationY.Dispose();
            WindowWidth.Dispose();
            WindowHeight.Dispose();

            ShowAlreadyDoneTasks.Dispose();

            ToggleWindowHotkey.Dispose();
            CheckboxType.Dispose();
        }
    }
}