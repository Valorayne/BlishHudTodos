using System;
using Blish_HUD;
using Todos.Source.Components;

namespace Todos.Source.Utils
{
    public class TodoVisualsManager : IDisposable
    {
        private TodoListWindow _window;
        private TodoCornerIcon _cornerIcon;

        public TodoVisualsManager()
        {
            Settings.WindowMinimized.SettingChanged += OnSettingChanged;
            GameService.Gw2Mumble.UI.IsMapOpenChanged += OnMapStatusChanged;
            GameService.GameIntegration.Gw2Instance.IsInGameChanged += OnInGameChanged;
            Settings.ShowWindowOnMap.SettingChanged += OnSettingChanged;
            Settings.AlwaysShowWindow.SettingChanged += OnSettingChanged;
        }

        private void OnInGameChanged(object sender, ValueEventArgs<bool> e)
        {
            UpdateDisplay();
        }

        public void OnModuleLoaded()
        {
            UpdateDisplay();
        }

        private void OnMapStatusChanged(object sender, ValueEventArgs<bool> valueEventArgs)
        {
            UpdateDisplay();
        }

        private void OnSettingChanged(object sender, ValueChangedEventArgs<bool> e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            var isInGame = GameService.GameIntegration.Gw2Instance.IsInGame;
            if (!isInGame && !Settings.AlwaysShowWindow.Value)
            {
                DisplayNothing();
                return;
            }
            
            var isInMap = GameService.Gw2Mumble.UI.IsMapOpen;
            if (isInMap && !Settings.ShowWindowOnMap.Value)
            {
                DisplayNothing();
                return;
            }

            if (Settings.WindowMinimized.Value)
                DisplayMinimized();
            else
                DisplayWindow();
        }

        private void DisplayNothing()
        {
            _cornerIcon?.Dispose();
            _cornerIcon = null;
            
            _window?.Dispose();
            _window = null;
        }

        private void DisplayWindow()
        {
            _cornerIcon?.Dispose();
            _cornerIcon = null;

            if (_window == null)
            {
                _window = new TodoListWindow();
                _window.Show();
            }
        }

        private void DisplayMinimized()
        {
            _window?.Dispose();
            _window = null;

            if (_cornerIcon == null)
            {
                _cornerIcon = new TodoCornerIcon();
                _cornerIcon.Show();
            }
        }

        public void Dispose()
        {
            Settings.WindowMinimized.SettingChanged -= OnSettingChanged;
            Settings.ShowWindowOnMap.SettingChanged -= OnSettingChanged;
            GameService.Gw2Mumble.UI.IsMapOpenChanged -= OnMapStatusChanged;
            GameService.GameIntegration.Gw2Instance.IsInGameChanged -= OnInGameChanged;
            _window?.Dispose();
            _cornerIcon?.Dispose();
        }
    }
}