using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly TodoCheckbox _checkbox;
        private readonly TodoTitle _todoTitle;
        private readonly HoverButton _editButton;
        private readonly HoverButton _deleteButton;

        private readonly Subscriptions.BackgroundTexture _hoverSubscription;

        public TodoEntry(Todo todo, int width)
        {
            Width = width;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            _checkbox = new TodoCheckbox { Parent = this };
            var titleWidth = width - _checkbox.Width - TodoEditButton.WIDTH - TodoDeleteButton.WIDTH;
            _todoTitle = new TodoTitle(todo, titleWidth) { Parent = this };
            _editButton = new TodoEditButton(todo) { Parent = this, Visible = false };
            _deleteButton = new TodoDeleteButton(todo) { Parent = this, Visible = false };

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;

            _hoverSubscription = new Subscriptions.BackgroundTexture(this, Resources.GetTexture(Textures.Header),
                Resources.GetTexture(Textures.HeaderHovered));
        }

        private void OnMouseEntered(object target, MouseEventArgs args)
        {
            _editButton.Show();
            _deleteButton.Show();
            RecalculateLayout();
        }
        
        private void OnMouseLeft(object target, MouseEventArgs args)
        {
            _editButton.Hide();
            _deleteButton.Hide();
            RecalculateLayout();
        }

        protected override void DisposeControl()
        {
            _hoverSubscription.Dispose();
            
            _checkbox.Dispose();
            _todoTitle.Dispose();
            _editButton.Dispose();
            _deleteButton.Dispose();

            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            
            base.DisposeControl();
        }
    }
}