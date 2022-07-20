using System;
using Blish_HUD;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public class TodoVisualsManager : IDisposable
    {
        private readonly TodoListModel _todoList;
        private readonly TodoWindowToggleHotkey _hotkeyManager; 
        
        private TodoListWindow _window;
        private TodoCornerIcon _cornerIcon;

        public TodoVisualsManager(TodoListModel todoList)
        {
            _todoList = todoList;
            _hotkeyManager = new TodoWindowToggleHotkey();

            GameService.Gw2Mumble.UI.IsMapOpenChanged += OnMapStatusChanged;
            GameService.GameIntegration.Gw2Instance.IsInGameChanged += OnInGameChanged;
            
            Settings.WindowMinimized.Subscribe(this, _ => UpdateDisplay());
            Settings.ShowWindowOnMap.Subscribe(this, _ => UpdateDisplay());
            Settings.AlwaysShowWindow.Subscribe(this, _ => UpdateDisplay());
        }

        private void OnInGameChanged(object sender, ValueEventArgs<bool> e) => UpdateDisplay();
        private void OnMapStatusChanged(object sender, ValueEventArgs<bool> valueEventArgs) => UpdateDisplay();

        public void OnModuleLoaded() => UpdateDisplay();

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
                _window = new TodoListWindow(_todoList);
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
            Settings.WindowMinimized.Unsubscribe(this);
            Settings.ShowWindowOnMap.Unsubscribe(this);
            Settings.AlwaysShowWindow.Unsubscribe(this);
            
            GameService.Gw2Mumble.UI.IsMapOpenChanged -= OnMapStatusChanged;
            GameService.GameIntegration.Gw2Instance.IsInGameChanged -= OnInGameChanged;
            
            _window?.Dispose();
            _cornerIcon?.Dispose();
            _hotkeyManager.Dispose();
        }
    }
}