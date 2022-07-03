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
        private readonly TodoEditButton _editButton;

        public event EventHandler<MouseEventArgs> HeaderClicked;

        public TodoHeader(Todo todo, int width)
        {
            BackgroundTexture = Resources.GetTexture(Textures.Header);
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox { Parent = this };
            _todoTitle = new TodoTitle(todo, width - _checkbox.Width - TodoEditButton.WIDTH) { Parent = this };
            _editButton = new TodoEditButton { Parent = this };
            _editButton.Hide();

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;
            _todoTitle.Click += OnMouseClick;
            _checkbox.Click += OnMouseClick;
        }

        private void OnMouseEntered(object target, MouseEventArgs args)
        {
            BackgroundTexture = Resources.GetTexture(Textures.HeaderHovered);
            _editButton.Show();
        }
        
        private void OnMouseLeft(object target, MouseEventArgs args)
        {
            BackgroundTexture = Resources.GetTexture(Textures.Header);
            _editButton.Hide();
        }

        private void OnMouseClick(object target, MouseEventArgs args)
        {
            _editButton.Expanded = !_editButton.Expanded;
            HeaderClicked?.Invoke(target, args);
        }

        protected override void DisposeControl()
        {
            _checkbox.Dispose();
            _todoTitle.Dispose();
            _editButton.Dispose();

            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            _todoTitle.Click -= OnMouseClick;
            _checkbox.Click -= OnMouseClick;
            
            base.DisposeControl();
        }
    }
}