using System;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public class TodoClipboardContentInput : TextBox
    {
        private static readonly Color DISABLED_COLOR = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        private readonly TodoModel _todo;

        public TodoClipboardContentInput(TodoModel todo)
        {
            _todo = todo;
            TextChanged += OnTextChanged;
            _todo.Schedule.ClipboardContent.Subscribe(this,
                clipboardContent =>
                {
                    Text = clipboardContent ?? _todo.ClipboardContent.Value;
                    BackgroundColor = clipboardContent == null ? Color.Transparent : DISABLED_COLOR;
                });
        }

        protected override CaptureType CapturesInput()
        {
            return _todo.Schedule.ClipboardContent.Value == null ? CaptureType.Mouse : CaptureType.None;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (_todo.Schedule.ClipboardContent.Value == null)
                _todo.ClipboardContent.Value = Text;
        }

        protected override void OnEnterPressed(EventArgs e)
        {
            _todo.IsEditing.Value = false;
            base.OnEnterPressed(e);
        }

        protected override void DisposeControl()
        {
            TextChanged -= OnTextChanged;
            _todo.Schedule.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}