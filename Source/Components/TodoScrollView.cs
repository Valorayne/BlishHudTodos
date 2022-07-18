using System;
using System.Collections.Generic;
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

            Data.VisibleTodos.Changed += OnVisibleTodosChanged;
            OnVisibleTodosChanged(Data.VisibleTodos.Value);
        }

        private void OnVisibleTodosChanged(IReadOnlyList<TodoModel> newValue)
        {
            _saveScroll();
            foreach (var entry in _entries.Values)
                entry.Dispose();
            _entries.Clear();
            foreach (var todo in newValue)
                _entries.Add(todo, new TodoEntry(todo, _saveScroll) { Parent = this });            
        }

        protected override void DisposeControl()
        {
            Data.VisibleTodos.Changed -= OnVisibleTodosChanged;

            foreach (var entry in _entries.Values)
                entry.Dispose();

            _entries.Clear();
            base.DisposeControl();
        }
    }
}