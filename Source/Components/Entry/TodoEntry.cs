using System;
using System.Linq;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Models;
using Todos.Source.Utils.Reactive;
using Todos.Source.Utils.Subscriptions;

namespace Todos.Source.Components.Entry
{
    public sealed class TodoEntry : Panel
    {
        private const float MOVING_OPACITY = 0.7f;

        private readonly TodoEntryHoverMenu _hoverMenu;
        private readonly HoverSubscription _hoverSubscription;
        private readonly TodoEntryRow _row;
        private readonly Action _saveScroll;
        private readonly TodoModel _todo;
        private readonly TodoListModel _todoList;

        public TodoEntry(SettingsModel settings, TodoListModel todoList, PopupModel popup, TodoModel todo,
            Action saveScroll)
        {
            _todoList = todoList;
            _todo = todo;
            _saveScroll = saveScroll;

            WidthSizingMode = SizingMode.Fill;

            _hoverMenu = new TodoEntryHoverMenu(todoList, todo, popup, _saveScroll) { Parent = this };
            _row = new TodoEntryRow(settings, todo, todoList, _hoverMenu) { Parent = this };
            _hoverMenu.ZIndex = _row.ZIndex + 1;
            Height = _row.Height;

            todo.IsEditing.Subscribe(this, OnEditModeChanged);
            _row.Resized += OnRowResized;

            _hoverSubscription = new HoverSubscription(this, () =>
            {
                if (_todoList.MovingTodo.Value != _todo)
                    _todoList.MovingTodo.Unset();

                _todoList.HoveredTodo.Set(_todo);

                if (!settings.LockAllTasks.Value)
                    _hoverMenu.Show();
            }, () =>
            {
                if (!_todo.IsEditing.Value && _todoList.MovingTodo.Value != _todo)
                    _hoverMenu.Hide();

                if (_todoList.HoveredTodo.Value == _todo)
                    _todoList.HoveredTodo.Unset();
            });

            _todoList.MovingTodo.Subscribe(this, move => Opacity = move == _todo ? MOVING_OPACITY : 1f);
        }

        private bool CanBeMovedUp => _todoList.VisibleTodos.Value.FirstOrDefault() != _todo;
        private bool CanBeMovedDown => _todoList.VisibleTodos.Value.LastOrDefault() != _todo;

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

        protected override void OnResized(ResizedEventArgs e)
        {
            if (_hoverMenu != null)
                _hoverMenu.Location = new Point(Width - _hoverMenu.Width, 0);
            base.OnResized(e);
        }

        protected override void OnMouseLeft(MouseEventArgs e)
        {
            if (_todoList.MovingTodo.Value == _todo)
            {
                if (e.MousePosition.Y <= AbsoluteBounds.Y && CanBeMovedUp)
                    _todoList.MoveUp(_todoList.MovingTodo.Value);
                else if (e.MousePosition.Y >= AbsoluteBounds.Y + AbsoluteBounds.Height && CanBeMovedDown)
                    _todoList.MoveDown(_todoList.MovingTodo.Value);
                else
                    _todoList.MovingTodo.Unset();
            }

            base.OnMouseLeft(e);
        }

        protected override void DisposeControl()
        {
            _row.Resized -= OnRowResized;
            _todo.Unsubscribe(this);
            _hoverSubscription.Dispose();
            _todoList.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}