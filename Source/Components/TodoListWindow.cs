using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Messages;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public class TodoListWindow : StandardWindow
    {
        private const int MIN_WIDTH = 300;
        private const int MAX_WIDTH = 1000;
        private const int MIN_HEIGHT = 150;
        private const int MAX_HEIGHT = 1000;

        private bool _hovered;

        private static Rectangle GetWindowRegion => new Rectangle(0, -28,
            Math.Max(MIN_WIDTH, Math.Min(MAX_WIDTH, Settings.WindowWidth.Value)),
            Math.Max(MIN_HEIGHT, Math.Min(MAX_HEIGHT, Settings.WindowHeight.Value)));
        private static Rectangle GetContentRegion => new Rectangle(0, -28, GetWindowRegion.Width, GetWindowRegion.Height + 33);

        public TodoListWindow() : base(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion)
        {
            Parent = GameService.Graphics.SpriteScreen;
            Title = "Todos";
            SavesPosition = true;
            Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3";
            CanResize = true;
            CanClose = false;
            Opacity = Settings.WindowOpacityWhenNotFocussed.Value;

            new TodoListPanel { Parent = this };

            Click += OnClick;
            Settings.WindowOpacityWhenNotFocussed.SettingChanged += OnOpacityChanged;
        }

        protected override void OnMouseEntered(MouseEventArgs e)
        {
            _hovered = true;
            Opacity = 1f;
            base.OnMouseEntered(e);
        }

        protected override void OnMouseLeft(MouseEventArgs e)
        {
            _hovered = false;
            Opacity = Settings.WindowOpacityWhenNotFocussed.Value;
            base.OnMouseLeft(e);
        }

        private void OnOpacityChanged(object sender, ValueChangedEventArgs<float> e)
        {
            if (!_hovered)
                Opacity = e.NewValue;
        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            ConfirmDeletionWindow.Hide();
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            // hacky check to prevent infinite recursive call
            if (!Size.Equals(new Point(GetWindowRegion.Width, GetWindowRegion.Height + 40)))
                ConstructWindow(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion);

            Settings.WindowWidth.Value = e.CurrentSize.X;
            Settings.WindowHeight.Value = e.CurrentSize.Y; 
            
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            Settings.WindowOpacityWhenNotFocussed.SettingChanged -= OnOpacityChanged;
            Click -= OnClick;
            base.DisposeControl();
        }
    }
}