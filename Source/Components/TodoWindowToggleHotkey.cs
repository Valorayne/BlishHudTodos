using System;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public class TodoWindowToggleHotkey : IDisposable
    {
        private readonly SettingsModel _settings;

        public TodoWindowToggleHotkey(SettingsModel settings)
        {
            _settings = settings;
            settings.ToggleWindowHotkey.Value.Enabled = true;
            settings.ToggleWindowHotkey.Value.Activated += OnHotkeyActivated;
        }

        private void OnHotkeyActivated(object sender, EventArgs e)
        {
            _settings.WindowMinimized.Value = !_settings.WindowMinimized.Value;
        }

        public void Dispose()
        {
            _settings.ToggleWindowHotkey.Value.Activated -= OnHotkeyActivated;
        }
    }
}