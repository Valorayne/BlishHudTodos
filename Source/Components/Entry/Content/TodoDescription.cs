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
            
            todo.DescriptionChanged += OnDescriptionChanged;
            EditField.TextChanged += OnChange;
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
            if (!_todo.ClipboardContent.IsNullOrEmpty())
            {
                _clipboardService.SetTextAsync(_todo.ClipboardContent)
                    .ContinueWith(_ => TooltipNotification.Spawn("Content copied to clipboard!", e.MousePosition));
            }
            base.OnClick(e);
        }

        private void OnChange(object sender, EventArgs e)
        {
            _todo.Description = EditField.Text;
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

        private void OnDescriptionChanged(string newDescription)
        {
            _label.Text = newDescription;
        }

        protected override void DisposeControl()
        {
            EditField.TextChanged -= OnChange;
            _todo.DescriptionChanged -= OnDescriptionChanged;
            base.DisposeControl();
        }
    }
}