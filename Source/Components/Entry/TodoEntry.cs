using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Components.Messages;
using Todos.Source.Models;
using Todos.Source.Utils;
using Utility = Todos.Source.Utils.Utility;

namespace Todos.Source.Components.Entry
{
    public sealed class TodoEntry : Panel
    {
        private readonly TodoModel _todo;
        private readonly Action _saveScroll;
        private readonly TodoEntryHoverMenu _hoverMenu;
        private readonly TodoEntryRow _row;

        public TodoEntry(TodoModel todo, Action saveScroll)
        {
            _todo = todo;
            _saveScroll = saveScroll;

            _hoverMenu = new TodoEntryHoverMenu(OnEdit, OnDelete) { Parent = this, Visible = false };
            _row = new TodoEntryRow(todo, _hoverMenu, OnEdit) { Parent = this };
            _hoverMenu.ZIndex = _row.ZIndex + 1;

            todo.DoneChanged += OnDoneChanged;

            if (todo.IsNew)
            {
                Utility.Delay(OnEdit);
                todo.IsNew = false;
            }
            
            WidthSizingMode = SizingMode.Fill;
            Height = _row.Height;

            _row.Resized += OnRowResized;
        }

        private void OnRowResized(object sender, ResizedEventArgs e)
        {
            _saveScroll();
            Height = _row.Height;
        }

        public bool IsEditing => _row.IsEditing;

        private void OnEdit()
        {
            if (!IsEditing)
                _hoverMenu.Show();
            else if (!MouseOver)
                _hoverMenu.Hide();
            
            _row.IsEditing = !_row.IsEditing;
            _hoverMenu.EditButton.IsEditing = _row.IsEditing;
            Height = _row.Height;

            if (_todo.Done && !IsEditing && !Settings.ShowAlreadyDoneTasks.Value)
            {
                Hide();
                Parent.Invalidate();
            }
        }

        private void OnDelete(Point location)
        {
            ConfirmDeletionWindow.Spawn(location, () =>
            {
                _saveScroll();
                _todo.Delete();
            });
        }

        private void OnDoneChanged(bool newDone)
        {
            if (newDone && !Settings.ShowAlreadyDoneTasks.Value && !IsEditing)
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
            if (!IsEditing)
                _hoverMenu.Hide();
            base.OnMouseLeft(e);
        }

        protected override void DisposeControl()
        {
            _row.Resized -= OnRowResized;
            _todo.DoneChanged -= OnDoneChanged;
            base.DisposeControl();
        }
    }
}