using System;
using Todos.Source.Utils;

namespace Todos.Source
{
    public class TodoWindowToggleHotkey : IDisposable
    {
        public TodoWindowToggleHotkey()
        {
            Settings.ToggleWindowHotkey.Value.Enabled = true;
            Settings.ToggleWindowHotkey.Value.Activated += OnHotkeyActivated;
        }

        private static void OnHotkeyActivated(object sender, EventArgs e)
        {
            Settings.WindowMinimized.Value = !Settings.WindowMinimized.Value;
        }

        public void Dispose()
        {
            Settings.ToggleWindowHotkey.Value.Activated -= OnHotkeyActivated;
        }
    }
}