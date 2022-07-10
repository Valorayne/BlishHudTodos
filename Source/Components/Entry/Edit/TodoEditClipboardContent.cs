using System;
using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public class TodoEditClipboardContent : TextBox
    {
        private readonly Todo _todo;

        public TodoEditClipboardContent(Todo todo)
        {
            _todo = todo;
            TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            _todo.ClipboardContent = Text;
            _todo.Save();
        }

        protected override void DisposeControl()
        {
            TextChanged -= OnTextChanged;
            base.DisposeControl();
        }
    }
}