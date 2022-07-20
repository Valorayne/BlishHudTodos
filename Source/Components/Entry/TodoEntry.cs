using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Components.Messages;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry
{
    public sealed class TodoEntry : Panel
    {
        public readonly TodoModel Todo;
        
        private readonly Action _saveScroll;
        private readonly TodoEntryHoverMenu _hoverMenu;
        private readonly TodoEntryRow _row;

        public TodoEntry(TodoListModel todoList, TodoModel todo, Action saveScroll)
        {
            Todo = todo;
            _saveScroll = saveScroll;

            _hoverMenu = new TodoEntryHoverMenu(todoList, todo, OnDelete) { Parent = this };
            _row = new TodoEntryRow(todo, _hoverMenu) { Parent = this };
            _hoverMenu.ZIndex = _row.ZIndex + 1;

            todo.IsEditing.Subscribe(this, OnEditModeChanged);
            
            WidthSizingMode = SizingMode.Fill;
            Height = _row.Height;

            _row.Resized += OnRowResized;
        }

        private void OnEditModeChanged(bool isInEditMode)
        {
            if (isInEditMode)
                _hoverMenu.Show();
            else if (!MouseOver)
                _hoverMenu.Hide();
            
            Height = _row.Height;
        }

        private void OnRowResized(object sender, ResizedEventArgs e)
        {
            _saveScroll();
            Height = _row.Height;
        }

        private void OnDelete(Point location)
        {
            ConfirmDeletionWindow.Spawn(location, () =>
            {
                _saveScroll();
                Todo.IsDeleted.Value = true;
            });
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
            if (!Todo.IsEditing.Value)
                _hoverMenu.Hide();
            base.OnMouseLeft(e);
        }

        protected override void DisposeControl()
        {
            _row.Resized -= OnRowResized;
            Todo.IsEditing.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}