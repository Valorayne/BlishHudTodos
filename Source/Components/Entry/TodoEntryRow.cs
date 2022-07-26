using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Content;
using Todos.Source.Components.Entry.Edit;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry
{
    public sealed class TodoEntryRow : FlowPanel
    {
        private readonly TodoModel _todo;
        private readonly TodoEntryContent _content;
        private readonly TodoEditPanel _editMenu;

        public TodoEntryRow(SettingsModel settings, TodoModel todo, TodoEntryHoverMenu hoverMenu)
        {
            _todo = todo;
            
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            
            _content = new TodoEntryContent(settings, todo, hoverMenu) { Parent = this, Location = Point.Zero };
            _editMenu = new TodoEditPanel(todo) { Parent = this, Location = new Point(0, HEADER_HEIGHT) };
            
            _editMenu.Resized += OnEditMenuResized;
            todo.IsEditing
                .Subscribe(this, v => _editMenu.Visible = v)
                .Subscribe(this, _ => UpdateHeight());
        }

        private void UpdateHeight() => Height = _content.Height + (_editMenu.Visible ? _editMenu.Height : 0);
        private void OnEditMenuResized(object sender, EventArgs eventArgs) => UpdateHeight();

        protected override void OnResized(ResizedEventArgs e)
        {
            UpdateHeight();
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            _todo.Unsubscribe(this);
            _editMenu.Resized -= OnEditMenuResized;
            base.DisposeControl();
        }
    }
}