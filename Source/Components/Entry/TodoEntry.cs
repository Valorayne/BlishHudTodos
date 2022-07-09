using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Content;
using Todos.Source.Components.Entry.Edit;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Components.Messages;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry
{
    public sealed class TodoEntry : Panel
    {
        private readonly Todo _todo;
        private readonly Action _saveScroll;
        private readonly TodoEntryHoverMenu _hoverMenu;
        private readonly TodoEditPanel _editMenu;
        private readonly TodoEntryContent _content;

        public TodoEntry(Todo todo, Action saveScroll)
        {
            _todo = todo;
            _saveScroll = saveScroll;
            
            WidthSizingMode = SizingMode.Fill;
            Height = HEADER_HEIGHT;

            _hoverMenu = new TodoEntryHoverMenu(OnEdit, OnDelete) { Parent = this, Visible = false };
            _content = new TodoEntryContent(todo, _hoverMenu) { Parent = this, Location = Point.Zero };
            _hoverMenu.ZIndex = _content.ZIndex + 1;
            _editMenu = new TodoEditPanel(todo) { Parent = this, Location = new Point(0, HEADER_HEIGHT) };

            _content.Description.EditField.EnterPressed += OnEnterPressed;
            Data.TodoModified += OnTodoModified;
            
            if (todo.IsNew)
                Utility.Delay(OnEdit);
        }

        private void OnEnterPressed(object sender, EventArgs e)
        {
            OnEdit();
        }

        public bool IsEditing => Height != HEADER_HEIGHT;

        private void OnEdit()
        {
            _saveScroll();
             Height = IsEditing ? HEADER_HEIGHT : HEADER_HEIGHT + _editMenu.Height;
            _hoverMenu.EditButton.IsEditing = IsEditing;
            _content.Description.IsEditing = IsEditing;
        }

        private void OnDelete(Point location)
        {
            ConfirmDeletionWindow.Spawn(location, () =>
            {
                _saveScroll();
                _todo.Delete();
            });
        }

        private void OnTodoModified(object sender, Todo todo)
        {
            if (todo == _todo && todo.Done && !Settings.ShowAlreadyDoneTasks.Value)
            {
                Hide();
                Parent.Invalidate();
            }
        }

        private void RepositionHoverMenu()
        {
            if (_hoverMenu != null)
                _hoverMenu.Location = new Point(Width - _hoverMenu.Width, 0);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            RepositionHoverMenu();
            base.OnResized(e);
        }

        protected override void OnMouseEntered(MouseEventArgs e)
        {
            _hoverMenu.Show();
            base.OnMouseEntered(e);
        }

        protected override void OnMouseLeft(MouseEventArgs e)
        {
            _hoverMenu.Hide();
            base.OnMouseLeft(e);
        }

        protected override void DisposeControl()
        {
            Data.TodoModified -= OnTodoModified;
            _content.Description.EditField.EnterPressed -= OnEnterPressed;
            base.DisposeControl();
        }
    }
}