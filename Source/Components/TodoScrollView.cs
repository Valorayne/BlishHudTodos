using System;
using System.Collections.Generic;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry;
using Todos.Source.Components.Messages;
using Todos.Source.Models;

namespace Todos.Source.Components
{
    public sealed class TodoScrollView : FlowPanel
    {
        private const int OUTER_PADDING = 5;
        private const int INNER_PADDING = 5;

        private readonly Dictionary<TodoModel, TodoEntry> _entries = new Dictionary<TodoModel, TodoEntry>();
        private readonly PopupModel _popup;
        private readonly Action _saveScroll;
        private readonly SettingsModel _settings;
        private readonly TodoListModel _todoList;

        public TodoScrollView(SettingsModel settings, TodoListModel todoList, PopupModel popup, Action saveScroll)
        {
            _settings = settings;
            _todoList = todoList;
            _popup = popup;
            _saveScroll = saveScroll;

            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = new Vector2(OUTER_PADDING, OUTER_PADDING);
            ControlPadding = new Vector2(INNER_PADDING, INNER_PADDING);

            new AllTodosDoneMessage(todoList) { Parent = this };
            new NoTodosYetMessage(todoList) { Parent = this };

            todoList.VisibleTodos.Subscribe(this, OnVisibleTodosChanged);
        }

        protected override CaptureType CapturesInput()
        {
            return _settings.ClickThroughBackground.Value ? CaptureType.MouseWheel : base.CapturesInput();
        }

        private void OnVisibleTodosChanged(IReadOnlyList<TodoModel> newValue)
        {
            _saveScroll();
            RemoveEntries();
            foreach (var todo in newValue)
                _entries.Add(todo, new TodoEntry(_settings, _todoList, _popup, todo, _saveScroll) { Parent = this });
        }

        private void RemoveEntries()
        {
            foreach (var entry in _entries.Values)
                entry.Dispose();
            _entries.Clear();
        }

        protected override void DisposeControl()
        {
            _todoList.Unsubscribe(this);
            RemoveEntries();
            base.DisposeControl();
        }
    }
}