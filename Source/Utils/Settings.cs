using Blish_HUD.Settings;

namespace TodoList
{
    public class Settings
    {
        public SettingEntry<bool> ShowMenuIcon { get; }

        public Settings(SettingCollection settings)
        {
            ShowMenuIcon = settings.DefineSetting("Menu.Icon.Show", true, () => "Show Menu Icon", () => "Whether or not the menu icon in the top left bar should be shown");
        }
    }
}