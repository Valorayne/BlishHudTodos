using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoHeader : FlowPanel
    {
        private readonly TodoCheckbox _checkbox;
        private readonly TodoTitle _todoTitle;
        private readonly TodoCollapseArrow _collapseArrow;

        public event EventHandler<MouseEventArgs> HeaderClicked;

        public TodoHeader(Todo todo, int width)
        {
            BackgroundTexture = Resources.GetTexture(Textures.Header);
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox { Parent = this };
            _todoTitle = new TodoTitle(todo, width - 2 * HEADER_HEIGHT) { Parent = this };
            _collapseArrow = new TodoCollapseArrow { Parent = this };

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;
            _todoTitle.Click += OnMouseClick;
            _collapseArrow.Click += OnMouseClick;
        }

        private void OnMouseEntered(object target, MouseEventArgs args)
        {
            BackgroundTexture = Resources.GetTexture(Textures.HeaderHovered);
        }
        
        private void OnMouseLeft(object target, MouseEventArgs args)
        {
            BackgroundTexture = Resources.GetTexture(Textures.Header);
        }

        private void OnMouseClick(object target, MouseEventArgs args)
        {
            _collapseArrow.Expanded = !_collapseArrow.Expanded;
            HeaderClicked?.Invoke(target, args);
        }

        protected override void DisposeControl()
        {
            _checkbox.Dispose();
            _todoTitle.Dispose();
            _collapseArrow.Dispose();

            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            _todoTitle.Click -= OnMouseClick;
            _collapseArrow.Click -= OnMouseClick;
            
            base.DisposeControl();
        }
    }
}