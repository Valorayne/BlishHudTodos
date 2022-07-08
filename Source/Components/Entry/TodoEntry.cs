using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using TodoList.Components.Details;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntry : Panel
    {
        private readonly Todo _todo;
        private readonly Action _saveScroll;
        private readonly TodoEntryHoverMenu _hoverMenu;
        private readonly TodoEditPanel _editMenu;

        public TodoEntry(Todo todo, Action saveScroll)
        {
            _todo = todo;
            _saveScroll = saveScroll;
            
            WidthSizingMode = SizingMode.Fill;
            Height = HEADER_HEIGHT;
            
            new TodoEntryContent(todo) { Parent = this, Location = Point.Zero };
            _hoverMenu = new TodoEntryHoverMenu(OnEdit, OnDelete) { Parent = this, Visible = false };
            _editMenu = new TodoEditPanel(todo) { Parent = this, Location = new Point(0, HEADER_HEIGHT) };

            Data.TodoModified += OnTodoModified;
            
            if (todo.IsNew)
                Utility.NextFrame(OnEdit);
        }

        public bool IsExpanded => Height != HEADER_HEIGHT;

        private void OnEdit()
        {
            _saveScroll();
            Height = IsExpanded ? HEADER_HEIGHT : HEADER_HEIGHT + _editMenu.Height;
            _hoverMenu.EditButton.IsExpanded = IsExpanded;
            _editMenu.Focus();
        }

        private void OnDelete()
        {
            _saveScroll();
            _todo.Delete();
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
            base.DisposeControl();
        }
    }
}