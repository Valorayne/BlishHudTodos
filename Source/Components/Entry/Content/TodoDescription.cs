using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class TodoDescription : Panel
    {
        private readonly Todo _todo;
        private readonly Label _label;
        
        public TextBox EditField { get; }

        public TodoDescription(Todo todo)
        {
            _todo = todo;
            Height = HEADER_HEIGHT;
            _label = new Label
            {
                Parent = this,
                StrokeText = true,
                Text = todo.Description,
                AutoSizeWidth = true,
                Location = new Point(0, 8)
            };
            EditField = new TextBox
            {
                Parent = this,
                Text = todo.Description,
                Visible = false,
                Location = new Point(0, 5)
            };
            Data.TodoModified += OnTodoModified;
            EditField.TextChanged += OnChange;
        }

        private void OnChange(object sender, EventArgs e)
        {
            _todo.Description = EditField.Text;
            _todo.Save();
        }

        public bool IsEditing
        {
            set
            {
                _label.Visible = !value;
                EditField.Visible = value;
                
                if (EditField.Visible)
                {
                    EditField.Focused = true;
                    EditField.SelectionStart = 0;
                    EditField.SelectionEnd = EditField.Text.Length;
                }
            }
        }

        private void OnTodoModified(object sender, Todo todo)
        {
            if (todo == _todo)
                _label.Text = todo.Description;
        }

        protected override void DisposeControl()
        {
            EditField.TextChanged -= OnChange;
            Data.TodoModified -= OnTodoModified;
            base.DisposeControl();
        }
    }
}