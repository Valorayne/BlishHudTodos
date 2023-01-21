using AsyncWindowsClipboard;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoDescriptionLabel : Label
    {
        private readonly WindowsClipboardService _clipboardService;
        private readonly TodoModel _todo;

        public TodoDescriptionLabel(TodoModel todo)
        {
            _todo = todo;
            _clipboardService = new WindowsClipboardService();

            StrokeText = true;
            Location = new Point(0, 8);

            todo.Description.Subscribe(this, v => Text = v);
            todo.ClipboardContent.Subscribe(this, v => BasicTooltipText = GetTooltip(v));
            todo.IsEditing.Subscribe(this, isEditing => Visible = !isEditing);
        }

        private static string GetTooltip(string clipboardContent)
        {
            return clipboardContent?.Trim().IsNullOrEmpty() ?? true ? null : "Click to copy to clipboard";
        }

        protected override void OnClick(MouseEventArgs e)
        {
            var clipboardContent = _todo.ClipboardContent.Value ?? _todo.Schedule.ClipboardContent.Value;
            if (!clipboardContent.IsNullOrEmpty())
                _clipboardService.SetTextAsync(_todo.Schedule.ClipboardContent.Value ?? _todo.ClipboardContent.Value)
                    .ContinueWith(_ => TooltipNotification.Spawn("Content copied to clipboard!", e.MousePosition));
            base.OnClick(e);
        }

        protected override void DisposeControl()
        {
            _todo.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}