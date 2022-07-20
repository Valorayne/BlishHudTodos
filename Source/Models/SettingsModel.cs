using Blish_HUD.Input;
using Blish_HUD.Settings;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Utils
{
    public class SettingsModel : ModelBase
    {
        public readonly IVariable<bool> WindowMinimized;
        public readonly IVariable<bool> AlwaysShowWindow;
        public readonly IVariable<bool> ShowWindowOnMap;
        public readonly IVariable<bool> FixatedWindow;

        public readonly IVariable<float> BackgroundOpacity;
        public readonly IVariable<float> WindowOpacityWhenNotFocussed;

        public readonly IVariable<int> WindowLocationX;
        public readonly IVariable<int> WindowLocationY;
        public readonly IVariable<int> WindowWidth;
        public readonly IVariable<int> WindowHeight;

        public readonly IVariable<bool> ShowAlreadyDoneTasks;

        public readonly IVariable<KeyBinding> ToggleWindowHotkey;
        public readonly IVariable<CheckboxType> CheckboxType;

        public SettingsModel(SettingCollection settings)
        {
            WindowMinimized = Add(settings.DefineSetting("Window.Visibility.Minimized", false).ToVariable());
            AlwaysShowWindow = Add(settings.DefineSetting("Window.Visibility.Always", false).ToVariable());
            ShowWindowOnMap = Add(settings.DefineSetting("Window.Visibility.OnMap", false).ToVariable());
            FixatedWindow = Add(settings.DefineSetting("Window.Location.Fixed", false).ToVariable());

            BackgroundOpacity = Add(settings.DefineSetting("Window.Background.Opacity", 0.2f).ToVariable());
            WindowOpacityWhenNotFocussed = Add(settings.DefineSetting("Window.Visibility.Opacity.WhenNotFocussed", 1f).ToVariable());

            WindowLocationX = Add(settings.DefineSetting("Window.Location.X", 200).ToVariable());
            WindowLocationY = Add(settings.DefineSetting("Window.Location.Y", 200).ToVariable());
            WindowWidth = Add(settings.DefineSetting("Window.Dimensions.Width", 400).ToVariable());
            WindowHeight = Add(settings.DefineSetting("Window.Dimensions.Height", 200).ToVariable());

            ShowAlreadyDoneTasks = Add(settings.DefineSetting("Menu.Bar.ShowAlreadyDoneTasks", true).ToVariable());

            ToggleWindowHotkey = Add(settings.DefineSetting("Hotkeys.Window.Toggle", new KeyBinding()).ToVariable());
            CheckboxType = Add(settings.DefineSetting("Checkbox.Type", Utils.CheckboxType.Standard).ToVariable());
        }
    }
}