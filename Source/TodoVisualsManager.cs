using System;
using Todos.Source.Components;
using Todos.Source.Models;
using Todos.Source.Utils.Reactive;

namespace Todos.Source
{
    public class TodoVisualsManager : IDisposable
    {
        private readonly TodoWindowToggleHotkey _hotkeyManager;
        private readonly IDisposable _showWindowSubscription;
        private readonly TodoListWindow _window;

        public TodoVisualsManager(SettingsModel settings, GameModel game, TodoListModel todoList, PopupModel popup)
        {
            _hotkeyManager = new TodoWindowToggleHotkey(settings);
            _window = new TodoListWindow(settings, todoList, popup);

            _showWindowSubscription = game.IsInGame.CombineWith(game.IsMapOpen, settings.WindowMinimized,
                settings.ShowWindowOnMap, settings.ShowWindowOutOfGame, ShouldHideWindow)
                .Subscribe(this, hide => _window.Visible = !hide);
        }

        private static bool ShouldHideWindow(bool inGame, bool mapOpen, bool minimized, bool showOnMap, bool alwaysShow) 
            => !inGame && !alwaysShow || mapOpen && !showOnMap || minimized;

        public void Dispose()
        {
            _window?.Dispose();
            _hotkeyManager.Dispose();
            _showWindowSubscription.Dispose();
        }
    }
}