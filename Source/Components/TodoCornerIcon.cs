using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;

namespace TodoList.Components
{
    public class TodoCornerIcon : IDisposable
    {
        private readonly CornerIcon _icon;
        private readonly Container _window;

        public TodoCornerIcon(Container window)
        {
            _window = window;
            _icon = CreateIcon();
            
            _icon.Click += OnIconClicked;
            Settings.ShowMenuIcon.SettingChanged += OnShowMenuIconChanged;
            OnShowMenuIconChanged(this, new ValueChangedEventArgs<bool>(false, Settings.ShowMenuIcon.Value));
        }

        private static CornerIcon CreateIcon()
        {
            return new CornerIcon
            {
                IconName = "Todo List",
                Icon = Resources.GetTexture(Textures.CornerIcon),
                HoverIcon = Resources.GetTexture(Textures.CornerIconHovered),
                Priority = 5
            };
        }

        private void OnIconClicked(object target, MouseEventArgs args)
        {
            if (_window.Visible)
                _window.Hide();
            else _window.Show();
        }

        private void OnShowMenuIconChanged(object target, ValueChangedEventArgs<bool> args)
        {
            if (args.NewValue)
                _icon.Show();
            else _icon.Hide();
        }

        public void Dispose()
        {
            _icon.Dispose();
            Settings.ShowMenuIcon.SettingChanged -= OnShowMenuIconChanged;
        }
    }
}