using System;
using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public class TodoEditClipboardContent : TextBox
    {
        private readonly TodoModel _todo;

        public TodoEditClipboardContent(TodoModel todo)
        {
            _todo = todo;
            Text = _todo.ClipboardContent;
            TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            _todo.ClipboardContent = Text;
        }

        protected override void DisposeControl()
        {
            TextChanged -= OnTextChanged;
            base.DisposeControl();
        }
    }
}