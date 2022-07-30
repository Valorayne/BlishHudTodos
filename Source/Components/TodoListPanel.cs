using System.ComponentModel;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todos.Source.Components.Menu;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public sealed class TodoListPanel : FlowPanel
    {
        private const int SCROLL_BAR_WIDTH = 15;

        private readonly TodoListModel _todoList;
        private readonly PopupModel _popup;
        private readonly Scrollbar _scrollBar;
        private readonly TodoListMenuBar _menuBar;
        private readonly TodoScrollView _scrollView;

        public TodoListPanel(SettingsModel settings, TodoListModel todoList, PopupModel popup)
        {
            _todoList = todoList;
            _popup = popup;

            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;

            _menuBar = new TodoListMenuBar(settings, todoList) { Parent = this };
            _scrollView = new TodoScrollView(settings, todoList, popup, SaveScroll) { Parent = this };
            _scrollBar = new Scrollbar(_scrollView) { Parent = this };

            _todoList.AllTodos.Subscribe(this, (before, after) =>
            {
                if (before.Count < after.Count)
                    Utility.Delay(() => _scrollBar.ScrollDistance = 1, 50);
            });

            _scrollBar.PropertyChanged += OnScrollPropertyChanged;
        }

        private void OnScrollPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ScrollDistance")
                _popup.Close();
        }

        private float? _scrollTarget;

        private void SaveScroll()
        {
            if (_scrollBar != null)
                _scrollTarget = _scrollBar.ScrollDistance * (_scrollView.Height - _scrollView.ContentBounds.Y);
        }
        
        public override void PaintBeforeChildren(SpriteBatch spriteBatch, Rectangle bounds)
        {
            base.PaintBeforeChildren(spriteBatch, bounds);
            if (_scrollTarget.HasValue)
            {
                var factor = _scrollView.Height - _scrollView.ContentBounds.Y;
                _scrollBar.ScrollDistance = factor != 0 ? _scrollTarget.Value / factor : 0;
                _scrollTarget = null;
            }
        }

        private void ResizeComponents()
        {
            if (_scrollBar != null)
            {
                _scrollBar.Height = Height - _menuBar.Height;
                _scrollBar.Location = new Point(Width - SCROLL_BAR_WIDTH, TodoListMenuBar.HEIGHT);
            }

            if (_scrollView != null)
            {
                _scrollView.Height = Height - _menuBar.Height;
                _scrollView.Width = Width - SCROLL_BAR_WIDTH;
                _scrollView.Location = new Point(0, TodoListMenuBar.HEIGHT);
            }
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            ResizeComponents();
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            _todoList.Unsubscribe(this);
            _scrollBar.PropertyChanged -= OnScrollPropertyChanged;
            base.DisposeControl();
        }
    }
}