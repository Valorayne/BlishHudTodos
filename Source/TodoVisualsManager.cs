using System;
using Blish_HUD;
using Todos.Source.Components;
using Todos.Source.Models;

namespace Todos.Source
{
    public class TodoVisualsManager : IDisposable
    {
        private readonly SettingsModel _settings;
        private readonly TodoListModel _todoList;
        private readonly TodoWindowToggleHotkey _hotkeyManager; 
        
        private TodoListWindow _window;
        
        public TodoVisualsManager(SettingsModel settings, TodoListModel todoList)
        {
            _settings = settings;
            _todoList = todoList;
            _hotkeyManager = new TodoWindowToggleHotkey(settings);

            GameService.Gw2Mumble.UI.IsMapOpenChanged += OnMapStatusChanged;
            GameService.GameIntegration.Gw2Instance.IsInGameChanged += OnInGameChanged;
            
            _settings.WindowMinimized.Subscribe(this, _ => UpdateDisplay());
            _settings.ShowWindowOnMap.Subscribe(this, _ => UpdateDisplay());
            _settings.AlwaysShowWindow.Subscribe(this, _ => UpdateDisplay());
        }

        private void OnInGameChanged(object sender, ValueEventArgs<bool> e) => UpdateDisplay();
        private void OnMapStatusChanged(object sender, ValueEventArgs<bool> valueEventArgs) => UpdateDisplay();

        public void OnModuleLoaded() => UpdateDisplay();

        private void UpdateDisplay()
        {
            var isInGame = GameService.GameIntegration.Gw2Instance.IsInGame;
            if (!isInGame && !_settings.AlwaysShowWindow.Value)
            {
                DisplayMinimized();
                return;
            }
            
            var isInMap = GameService.Gw2Mumble.UI.IsMapOpen;
            if (isInMap && !_settings.ShowWindowOnMap.Value)
            {
                DisplayMinimized();
                return;
            }

            if (_settings.WindowMinimized.Value)
                DisplayMinimized();
            else
                DisplayWindow();
        }

        private void DisplayWindow()
        {
            if (_window == null)
                _window = new TodoListWindow(_settings, _todoList);
        }

        private void DisplayMinimized()
        {
            _window?.Dispose();
            _window = null;
        }

        public void Dispose()
        {
            _settings.Unsubscribe(this);
            
            GameService.Gw2Mumble.UI.IsMapOpenChanged -= OnMapStatusChanged;
            GameService.GameIntegration.Gw2Instance.IsInGameChanged -= OnInGameChanged;
            
            _window?.Dispose();
            _hotkeyManager.Dispose();
        }
    }
}