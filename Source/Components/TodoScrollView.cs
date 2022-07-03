using System.Collections.Generic;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoScrollView : FlowPanel
    {
        private const int OUTER_PADDING = 5;
        private const int INNER_PADDING = 5;
        private const int SCROLLBAR_WIDTH = 20;
        
        private readonly List<TodoEntry> _entries = new List<TodoEntry>();
        
        public TodoScrollView()
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            OuterControlPadding = new Vector2(OUTER_PADDING, OUTER_PADDING);
            ControlPadding = new Vector2(INNER_PADDING, INNER_PADDING);

            SpawnEntries();

            Data.TodoAdded += RedrawAllEntries;
        }

        private void RedrawAllEntries(object sender, Todo todo)
        {
            ClearEntries();
            SpawnEntries();
        }

        private void SpawnEntries()
        {
            var width = Settings.OverlayWidth.Value - 2 * OUTER_PADDING - SCROLLBAR_WIDTH;
            foreach (var todo in Data.Todos)
                _entries.Add(new TodoEntry(todo, width) { Parent = this });
        }

        private void ClearEntries()
        {
            foreach (var entry in _entries)
                entry.Dispose();
            _entries.Clear();
        }

        protected override void DisposeControl()
        {
            ClearEntries();
            base.DisposeControl();
        }
    }
}