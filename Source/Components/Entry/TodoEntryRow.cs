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
        private readonly Action _onEdit;
        private readonly TodoEntryContent _content;
        private readonly TodoEditPanel _editMenu;

        public TodoEntryRow(TodoModel todo, TodoEntryHoverMenu hoverMenu, Action onEdit)
        {
            _onEdit = onEdit;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            
            _content = new TodoEntryContent(todo, hoverMenu) { Parent = this, Location = Point.Zero };
            _editMenu = new TodoEditPanel(todo) { Parent = this, Location = new Point(0, HEADER_HEIGHT), Visible = false };

            _content.Description.EditField.EnterPressed += OnEnterPressed;
        }

        private void OnEnterPressed(object sender, EventArgs e)
        {
            _onEdit();
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            Height = _content.Height + (_editMenu.Visible ? _editMenu.Height : 0);
            base.OnResized(e);
        }

        public bool IsEditing
        {
            get => _editMenu.Visible;
            set
            {
                _editMenu.Visible = value;
                _content.Description.IsEditing = value;
                Height = _content.Height + (_editMenu.Visible ? _editMenu.Height : 0);
            }
        }

        protected override void DisposeControl()
        {
            _content.Description.EditField.EnterPressed += OnEnterPressed;
            base.DisposeControl();
        }
    }
}