using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Content;
using Todos.Source.Components.Entry.Edit;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry
{
    public sealed class TodoEntryRow : FlowPanel
    {
        private readonly TodoModel _todo;
        private readonly TodoEntryContent _content;
        private readonly TodoEditPanel _editMenu;

        public TodoEntryRow(TodoModel todo, TodoEntryHoverMenu hoverMenu)
        {
            _todo = todo;
            _content = new TodoEntryContent(todo, hoverMenu) { Parent = this, Location = Point.Zero };
            _editMenu = new TodoEditPanel(todo) { Parent = this, Location = new Point(0, HEADER_HEIGHT) };
            
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            UpdateHeight();

            _content.Description.EditField.EnterPressed += OnEnterPressed;
            _editMenu.Resized += OnEditMenuResized;

            todo.IsEditing.Changed += OnEditMenuChanged;
            OnEditMenuChanged(todo.IsEditing.Value);
        }

        private void OnEditMenuChanged(bool isInEditMode)
        {
            _editMenu.Visible = isInEditMode;
            UpdateHeight();
        }

        private void OnEditMenuResized(object sender, EventArgs eventArgs)
        {
            UpdateHeight();
        }

        private void OnEnterPressed(object sender, EventArgs e)
        {
            _todo.IsEditing.Value = false;
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            UpdateHeight();
            base.OnResized(e);
        }

        private void UpdateHeight()
        {
            Height = _content.Height + (_editMenu.Visible ? _editMenu.Height : 0);
        }

        protected override void DisposeControl()
        {
            _todo.IsEditing.Changed -= OnEditMenuChanged;
            _editMenu.Resized -= OnEditMenuResized;
            _content.Description.EditField.EnterPressed += OnEnterPressed;
            base.DisposeControl();
        }
    }
}