using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Todo_List
{
    public class TodoCornerIcon : IDisposable
    {
        public interface IResources
        {
            Texture2D CornerIcon { get; }
            Texture2D CornerIconHovered { get; }
        }
        
        private readonly CornerIcon _icon;
        private readonly IWindow _window;

        public TodoCornerIcon(IResources resources, IWindow window)
        {
            _window = window;
            _icon = new CornerIcon
            {
                IconName = "Todo List",
                Icon = resources.CornerIcon,
                HoverIcon = resources.CornerIconHovered,
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