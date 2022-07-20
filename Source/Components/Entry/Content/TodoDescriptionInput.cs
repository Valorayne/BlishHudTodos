using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoDescriptionInput : TextBox
    {
        private readonly TodoModel _todo;

        public TodoDescriptionInput(TodoModel todo)
        {
            _todo = todo;
            Text = todo.Description.Value;
            Location = new Point(0, 5);
            
            todo.IsEditing.Subscribe(this, isEditing => Visible = isEditing).Subscribe(this, isEditing =>
            {
                if (isEditing)
                {
                    Focused = true;
                    SelectionStart = 0;
                    SelectionEnd = Text.Length;
                }
            });

            TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e) => _todo.Description.Value = Text;

        protected override void OnEnterPressed(EventArgs e)
        {
            _todo.IsEditing.Value = false;
            base.OnEnterPressed(e);
        }
        
        protected override void DisposeControl()
        {
            _todo.IsEditing.Unsubscribe(this);
            TextChanged -= OnTextChanged;
            base.DisposeControl();
        }
    }
}