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
        
        private readonly Dictionary<Todo, TodoEntry> _entries = new Dictionary<Todo, TodoEntry>();
        private readonly int _width;
        
        public TodoScrollView()
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            OuterControlPadding = new Vector2(OUTER_PADDING, OUTER_PADDING);
            ControlPadding = new Vector2(INNER_PADDING, INNER_PADDING);

            _width = Settings.OverlayWidth.Value - 2 * OUTER_PADDING - SCROLLBAR_WIDTH;
            
            foreach (var todo in Data.Todos)
                SpawnEntry(this, todo);

            Data.TodoAdded += SpawnEntry;
            Data.TodoDeleted += DeleteEntry;
        }

        private void DeleteEntry(object sender, Todo todo)
        {
            _entries[todo].Dispose();
            _entries.Remove(todo);
        }

        private void SpawnEntry(object sender, Todo todo)
        {
            _entries.Add(todo, new TodoEntry(todo, _width) { Parent = this });
        }

        protected override void DisposeControl()
        {
            foreach (var entry in _entries.Values)
                entry.Dispose();
            _entries.Clear();
            base.DisposeControl();
        }
    }
}