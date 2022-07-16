﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry;
using Todos.Source.Components.Messages;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public sealed class TodoScrollView : FlowPanel
    {
        private readonly Action _saveScroll;
        
        private const int OUTER_PADDING = 5;
        private const int INNER_PADDING = 5;
        
        private readonly Dictionary<TodoModel, TodoEntry> _entries = new Dictionary<TodoModel, TodoEntry>();
        
        public TodoScrollView(Action saveScroll)
        {
            _saveScroll = saveScroll;
            
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = new Vector2(OUTER_PADDING, OUTER_PADDING);
            ControlPadding = new Vector2(INNER_PADDING, INNER_PADDING);

            new AllTodosDoneMessage { Parent = this };
            new NoTodosYetMessage { Parent = this };

            foreach (var todo in Data.Todos)
                SpawnEntry(this, todo);

            Data.TodoAdded += SpawnEntry;
            Data.TodoDeleted += DeleteEntry;
            Settings.ShowAlreadyDoneTasks.SettingChanged += ShowOrHideAlreadyDoneTasks;
            TimeService.NewMinute += OnNewMinute;
            
            UpdateReorderOptions();
        }

        private void UpdateReorderOptions()
        {
            var visibleEntries = _entries
                .Where(entry => entry.Value.Visible)
                .Select(entry => entry.Key)
                .ToList();
            
            for (var i = 0; i < visibleEntries.Count; i++)
            {
                visibleEntries[i].CanBeMovedUp.Value = i-1 >= 0;
                visibleEntries[i].CanBeMovedDown.Value = i+1 < visibleEntries.Count;
            }
        }

        private void OnNewMinute(object sender, GameTime e)
        {
            UpdateVisibilityOfChildren();
        }

        private void ShowOrHideAlreadyDoneTasks(object sender, ValueChangedEventArgs<bool> change)
        {
            UpdateVisibilityOfChildren();
        }

        private void UpdateVisibilityOfChildren()
        {
            foreach (var entry in _entries)
                entry.Value.Visible = !entry.Key.Done || Settings.ShowAlreadyDoneTasks.Value || entry.Key.IsEditing.Value;
            UpdateReorderOptions();
            RecalculateLayout();
        }

        private void DeleteEntry(object sender, TodoModel todo)
        {
            _entries[todo].Dispose();
            _entries.Remove(todo);
            UpdateReorderOptions();
        }

        private void SpawnEntry(object sender, TodoModel todo)
        {
            _entries.Add(todo, new TodoEntry(todo, _saveScroll)
            {
                Parent = this,
                Visible = Settings.ShowAlreadyDoneTasks.Value || !todo.Done,
            });
            if (sender != this)
                UpdateReorderOptions();
        }

        protected override void DisposeControl()
        {
            TimeService.NewMinute -= OnNewMinute;
            Settings.ShowAlreadyDoneTasks.SettingChanged -= ShowOrHideAlreadyDoneTasks;
            Data.TodoAdded -= SpawnEntry;
            Data.TodoDeleted -= DeleteEntry;

            foreach (var entry in _entries.Values)
                entry.Dispose();

            _entries.Clear();
            base.DisposeControl();
        }
    }
}