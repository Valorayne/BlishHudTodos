using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Components.Messages;
using Todos.Source.Models;
using Todos.Source.Utils;
using Todos.Source.Utils.Subscriptions;

namespace Todos.Source.Components.Entry
{
    public sealed class TodoEntry : Panel
    {
        private readonly TodoModel _todo;
        private readonly Action _saveScroll;
        
        private readonly TodoEntryHoverMenu _hoverMenu;
        private readonly TodoEntryRow _row;
        private readonly HoverSubscription _hoverSubscription;

        public TodoEntry(SettingsModel settings, TodoListModel todoList, TodoModel todo, Action saveScroll)
        {
            _todo = todo;
            _saveScroll = saveScroll;
            
            WidthSizingMode = SizingMode.Fill;

            _hoverMenu = new TodoEntryHoverMenu(todoList, todo, OnDelete) { Parent = this };
            _row = new TodoEntryRow(settings, todo, _hoverMenu) { Parent = this };
            _hoverMenu.ZIndex = _row.ZIndex + 1;
            Height = _row.Height;

            todo.IsEditing.Subscribe(this, OnEditModeChanged);
            _row.Resized += OnRowResized;

            _hoverSubscription = new HoverSubscription(this, _hoverMenu.Show, () =>
            {
                if (!_todo.IsEditing.Value)
                    _hoverMenu.Hide();
            });
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
                _todo.IsDeleted.Value = true;
            });
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (_hoverMenu != null)
                _hoverMenu.Location = new Point(Width - _hoverMenu.Width, 0);
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            _row.Resized -= OnRowResized;
            _todo.IsEditing.Unsubscribe(this);
            _hoverSubscription.Dispose();
            base.DisposeControl();
        }
    }
}