using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly Todo _todo;
        
        private TodoCheckbox _checkbox;
        private TodoTitle _todoTitle;
        private TodoEditButton _editButton;

        public TodoEntry(Todo todo, int width)
        {
            _todo = todo;
            Width = width;
            BackgroundTexture = Resources.GetTexture(Textures.Header);
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;

            SpawnComponents(todo, width);

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;

            Data.TodoModified += OnTodoModified;
        }

        private void OnTodoModified(object sender, Todo todo)
        {
            if (todo == _todo)
            {
                DespawnComponents();
                SpawnComponents(todo, Width);
            }
        }

        private void SpawnComponents(Todo todo, int width)
        {
            _checkbox = new TodoCheckbox { Parent = this };
            _todoTitle = new TodoTitle(todo, width - _checkbox.Width - TodoEditButton.WIDTH) { Parent = this };
            _editButton = new TodoEditButton(todo) { Parent = this, Visible = false };
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

        private void DespawnComponents()
        {
            _checkbox.Dispose();
            _todoTitle.Dispose();
            _editButton.Dispose();
        }

        protected override void DisposeControl()
        {
            DespawnComponents();

            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            
            base.DisposeControl();
        }
    }
}