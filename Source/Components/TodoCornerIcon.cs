using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;

namespace TodoList.Components
{
    public class TodoCornerIcon : IDisposable
    {
        private readonly CornerIcon _icon;
        private readonly Container _window;

        public TodoCornerIcon(Resources resources, Container window)
        {
            _window = window;
            _icon = new CornerIcon
            {
                IconName = "Todo List",
                Icon = resources.GetTexture(Textures.CornerIcon),
                HoverIcon = resources.GetTexture(Textures.CornerIconHovered),
                Priority = 5
            };
            _icon.Click += OnIconClicked;
        }

        private void OnIconClicked(object target, MouseEventArgs args)
        {
            if (_window.Visible)
                _window.Hide();
            else _window.Show();
        }

        public void Initialize()
        {
            _icon.Show();
        }
        
        public void Dispose()
        {
            _icon.Dispose();
        }
    }
}