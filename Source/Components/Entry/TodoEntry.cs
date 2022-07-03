using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly TodoCheckbox _checkbox;
        private readonly TodoTitle _todoTitle;
        private readonly TodoEditButton _editButton;

        public TodoEntry(Todo todo, int width)
        {
            Width = width;
            BackgroundTexture = Resources.GetTexture(Textures.Header);
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox { Parent = this };
            _todoTitle = new TodoTitle(todo, width - _checkbox.Width - TodoEditButton.WIDTH) { Parent = this };
            _editButton = new TodoEditButton(todo) { Parent = this, Visible = false };

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;
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

        protected override void DisposeControl()
        {
            _checkbox.Dispose();
            _todoTitle.Dispose();
            _editButton.Dispose();

            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            
            base.DisposeControl();
        }
    }
}