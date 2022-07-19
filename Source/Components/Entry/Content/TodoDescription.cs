using System;
using AsyncWindowsClipboard;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Menu;
using Todos.Source.Components.Generic;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Content
{
    public sealed class TodoDescription : Panel
    {
        private const int PADDING_RIGHT = 5;
        
        private readonly TodoModel _todo;
        private readonly TodoEntryHoverMenu _hoverMenu;
        private readonly Label _label;
        private readonly WindowsClipboardService _clipboardService;
        
        public TextBox EditField { get; }

        public TodoDescription(TodoModel todo, TodoEntryHoverMenu hoverMenu)
        {
            _todo = todo;
            _hoverMenu = hoverMenu;
            _clipboardService = new WindowsClipboardService();
            
            Height = HEADER_HEIGHT;
            
            _label = new Label
            {
                Parent = this,
                StrokeText = true,
                Text = todo.Description.Value,
                AutoSizeWidth = true,
                Location = new Point(0, 8)
            };
            EditField = new TextBox
            {
                Parent = this,
                Text = todo.Description.Value,
                Location = new Point(0, 5)
            };

            EditField.TextChanged += OnChange;
            todo.Description.Subscribe(this, v => _label.Text = v);
            todo.IsEditing.Subscribe(this, OnEditModeChanged);
            todo.ClipboardContent.Subscribe(this, v => 
                _label.BasicTooltipText = v?.Trim()?.IsNullOrEmpty() ?? true ? null : "Click to copy to clipboard"
            );
        }

        private void OnEditModeChanged(bool isInEditMode)
        {
            _label.Visible = !isInEditMode;
            EditField.Visible = isInEditMode;
                
            if (EditField.Visible)
            {
                EditField.Focused = true;
                EditField.SelectionStart = 0;
                EditField.SelectionEnd = EditField.Text.Length;
            }
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (EditField != null)
                EditField.Width = Width - _hoverMenu.Width - PADDING_RIGHT;
            if (_label != null)
                _label.Width = Width - _hoverMenu.Width;
            base.OnResized(e);
        }
        
        protected override void OnClick(MouseEventArgs e)
        {
            if (!_todo.ClipboardContent.Value.IsNullOrEmpty())
            {
                _clipboardService.SetTextAsync(_todo.ClipboardContent.Value)
                    .ContinueWith(_ => TooltipNotification.Spawn("Content copied to clipboard!", e.MousePosition));
            }
            base.OnClick(e);
        }

        private void OnChange(object sender, EventArgs e)
        {
            _todo.Description.Value = EditField.Text;
        }

        protected override void DisposeControl()
        {
            EditField.TextChanged -= OnChange;
            _todo.IsEditing.Unsubscribe(this);
            _todo.Description.Unsubscribe(this);
            _todo.ClipboardContent.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}