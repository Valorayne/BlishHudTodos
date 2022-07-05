using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly TodoCheckbox _checkbox;
        private readonly HoverButton _editButton;
        private readonly HoverButton _deleteButton;

        private readonly BackgroundTextureSubscription _hoverSubscription;

        public event EventHandler<bool> VisibilityChanged;

        public TodoEntry(Todo todo)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            
            _checkbox = new TodoCheckbox(todo) { Parent = this };
            new TodoScheduleIcon(todo) {Parent = this};
            new TodoTitle(todo) { Parent = this };
            _editButton = new TodoEditButton(todo) { Parent = this, Visible = false };
            _deleteButton = new TodoDeleteButton(todo) { Parent = this, Visible = false, Right = 0 };

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;

            _hoverSubscription = new BackgroundTextureSubscription(this, Resources.GetTexture(Textures.Header),
                Resources.GetTexture(Textures.HeaderHovered));

            _checkbox.Changed += OnCheckboxChanged;
        }

        private void OnCheckboxChanged(object sender, bool done)
        {
            if (done && !Settings.ShowAlreadyDoneTasks.Value)
            {
                Hide();
                VisibilityChanged?.Invoke(this, false);
            }
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

            _checkbox.Changed -= OnCheckboxChanged;

            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            
            base.DisposeControl();
        }
    }
}