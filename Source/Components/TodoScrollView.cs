using System.Collections.Generic;
using System.Linq;
using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoScrollView : FlowPanel
    {
        private const int OUTER_PADDING = 5;
        private const int INNER_PADDING = 5;
        
        private readonly Dictionary<Todo, TodoEntry> _entries = new Dictionary<Todo, TodoEntry>();
        
        public TodoScrollView()
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = new Vector2(OUTER_PADDING, OUTER_PADDING);
            ControlPadding = new Vector2(INNER_PADDING, INNER_PADDING);

            foreach (var todo in Data.Todos)
                SpawnEntry(this, todo);

            Data.TodoAdded += SpawnEntry;
            Data.TodoDeleted += DeleteEntry;
            Settings.ShowAlreadyDoneTasks.SettingChanged += ShowOrHideAlreadyDoneTasks;
        }

        private void ShowOrHideAlreadyDoneTasks(object sender, ValueChangedEventArgs<bool> change)
        {
            foreach (var entry in _entries.Where(entry => entry.Key.Done))
                entry.Value.Visible = change.NewValue;
            RecalculateLayout();
        }

        private void DeleteEntry(object sender, Todo todo)
        {
            _entries[todo].Dispose();
            _entries.Remove(todo);
        }

        private void SpawnEntry(object sender, Todo todo)
        {
            _entries.Add(todo, new TodoEntry(todo)
            {
                Parent = this,
                Visible = Settings.ShowAlreadyDoneTasks.Value || !todo.Done
            });
        }

        protected override void DisposeControl()
        {
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