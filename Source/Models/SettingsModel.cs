using Blish_HUD.Input;
using Blish_HUD.Settings;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Models
{
    public class SettingsModel : ModelBase
    {
        public readonly IVariable<float> BackgroundOpacity;
        public readonly IVariable<CheckboxType> CheckboxType;
        public readonly IVariable<bool> ClickThroughBackground;
        public readonly IVariable<bool> FixatedWindow;
        public readonly IVariable<bool> LockAllTasks;

        public readonly IVariable<bool> ShowAlreadyDoneTasks;
        public readonly IVariable<bool> ShowWindowOnMap;
        public readonly IVariable<bool> ShowWindowOutOfGame;

        public readonly IVariable<KeyBinding> ToggleWindowHotkey;
        public readonly IVariable<int> WindowHeight;

        public readonly IVariable<int> WindowLocationX;
        public readonly IVariable<int> WindowLocationY;
        public readonly IVariable<bool> WindowMinimized;
        public readonly IVariable<float> WindowOpacityWhenNotFocussed;
        public readonly IVariable<string> WindowTitle;
        public readonly IVariable<int> WindowWidth;

        public SettingsModel(SettingCollection settings)
        {
            WindowMinimized = Add(settings.DefineSetting("Window.Visibility.Minimized", false).ToVariable());
            ShowWindowOutOfGame = Add(settings.DefineSetting("Window.Visibility.Always", false).ToVariable());
            ShowWindowOnMap = Add(settings.DefineSetting("Window.Visibility.OnMap", false).ToVariable());
            FixatedWindow = Add(settings.DefineSetting("Window.Location.Fixed", false).ToVariable());
            WindowTitle = Add(settings.DefineSetting("Window.Title", "To-Dos").ToVariable());

            BackgroundOpacity = Add(settings.DefineSetting("Window.Background.Opacity", 0.2f).ToVariable());
            WindowOpacityWhenNotFocussed =
                Add(settings.DefineSetting("Window.Visibility.Opacity.WhenNotFocussed", 1f).ToVariable());
            ClickThroughBackground = Add(settings.DefineSetting("Window.Background.ClickThrough", false).ToVariable());

            WindowLocationX = Add(settings.DefineSetting("Window.Location.X", 200).ToVariable());
            WindowLocationY = Add(settings.DefineSetting("Window.Location.Y", 200).ToVariable());
            WindowWidth = Add(settings.DefineSetting("Window.Dimensions.Width", 400).ToVariable());
            WindowHeight = Add(settings.DefineSetting("Window.Dimensions.Height", 200).ToVariable());

            ShowAlreadyDoneTasks = Add(settings.DefineSetting("Menu.Bar.ShowAlreadyDoneTasks", true).ToVariable());
            LockAllTasks = Add(settings.DefineSetting("Menu.Bar.LockAllTasks", false).ToVariable());

            ToggleWindowHotkey = Add(settings.DefineSetting("Hotkeys.Window.Toggle", new KeyBinding()).ToVariable());
            CheckboxType = Add(settings.DefineSetting("Checkbox.Type", Utils.CheckboxType.Standard).ToVariable());
        }
    }
}