using System;
using Blish_HUD;
using Blish_HUD.Controls;

namespace TodoList
{
    public static class Subscriptions
    {
        public class BackgroundColor : IDisposable
        {
            private readonly Control _reference;

            public BackgroundColor(Control reference)
            {
                _reference = reference;
            }
            
            private void OnBackgroundColorsChanged(object target, ValueChangedEventArgs<float> args)
            {
                _reference.BackgroundColor = Settings.OverlayBackgroundColor;
            }

            public void Dispose()
            {
                Settings.OverlayBackgroundRed.SettingChanged -= OnBackgroundColorsChanged;
                Settings.OverlayBackgroundGreen.SettingChanged -= OnBackgroundColorsChanged;
                Settings.OverlayBackgroundBlue.SettingChanged -= OnBackgroundColorsChanged;
                Settings.OverlayBackgroundAlpha.SettingChanged -= OnBackgroundColorsChanged;
            }
        }
    }
}