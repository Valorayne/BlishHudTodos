using System;
using AsyncWindowsClipboard;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Models;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoDescriptionLabel : Label
    {
        private readonly WindowsClipboardService _clipboardService;
        private readonly TodoModel _todo;
        private readonly IDisposable _tooltip;

        public TodoDescriptionLabel(TodoModel todo)
        {
            _todo = todo;
            _clipboardService = new WindowsClipboardService();

            StrokeText = true;
            Location = new Point(0, 8);

            todo.Description.Subscribe(this, v => Text = v);
            _tooltip = todo.Schedule.ClipboardContent.CombineWith(todo.ClipboardContent, (a, b) => a ?? b)
                .Subscribe(this, v => BasicTooltipText = GetTooltip(v));
            todo.IsEditing.Subscribe(this, isEditing => Visible = !isEditing);
        }

        private static string GetTooltip(string clipboardContent)
        {
            return clipboardContent?.Trim().IsNullOrEmpty() ?? true ? null : "Click to copy to clipboard";
        }

        protected override void OnClick(MouseEventArgs e)
        {
            var clipboardContent = _todo.Schedule.ClipboardContent.Value ?? _todo.ClipboardContent.Value;
            if (!clipboardContent.IsNullOrEmpty())
                _clipboardService.SetTextAsync(clipboardContent)
                    .ContinueWith(_ => TooltipNotification.Spawn("Content copied to clipboard!", e.MousePosition));
            base.OnClick(e);
        }

        protected override void DisposeControl()
        {
            _todo.Unsubscribe(this);
            _tooltip.Dispose();
            base.DisposeControl();
        }
    }
}