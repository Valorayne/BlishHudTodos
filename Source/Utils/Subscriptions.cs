using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework.Graphics;

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
        
        public class BackgroundTexture : IDisposable
        {
            private readonly Panel _reference;
            private readonly Texture2D _normal;
            private readonly Texture2D _hovered;

            public BackgroundTexture(Panel reference, Texture2D normal, Texture2D hovered)
            {
                _reference = reference;
                _normal = normal;
                _hovered = hovered;

                _reference.BackgroundTexture = _normal;
                _reference.MouseEntered += OnMouseEntered;
                _reference.MouseLeft += OnMouseLeft;
            }

            private void OnMouseLeft(object sender, MouseEventArgs e)
            {
                _reference.BackgroundTexture = _normal;
            }

            private void OnMouseEntered(object sender, MouseEventArgs e)
            {
                _reference.BackgroundTexture = _hovered;
            }

            public void Dispose()
            {
                _reference.MouseEntered -= OnMouseEntered;
                _reference.MouseLeft -= OnMouseLeft;
            }
        }
    }
}