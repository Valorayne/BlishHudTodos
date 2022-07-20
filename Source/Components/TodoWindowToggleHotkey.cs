using System;
using Todos.Source.Models;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

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

        private void OnHotkeyActivated(object sender, EventArgs e) => _settings.WindowMinimized.Toggle();

        public void Dispose()
        {
            _settings.ToggleWindowHotkey.Value.Activated -= OnHotkeyActivated;
        }
    }
}