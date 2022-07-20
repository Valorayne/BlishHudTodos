using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Messages;
using Todos.Source.Models;
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

        public TodoListWindow(TodoListModel todoList) : base(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion)
        {
            Parent = GameService.Graphics.SpriteScreen;
            Title = "To-Dos";
            CanResize = !Settings.FixatedWindow.Value;
            CanClose = false;
            Opacity = Settings.WindowOpacityWhenNotFocussed.Value;
            Location = new Point(Settings.WindowLocationX.Value, Settings.WindowLocationY.Value);
            Visible = true;

            new TodoListPanel(todoList) { Parent = this };

            Settings.WindowOpacityWhenNotFocussed.Subscribe(this, newOpacity => { if (!_hovered) Opacity = newOpacity; });
            Settings.FixatedWindow.Subscribe(this, fixated => CanResize = !fixated);

            BackgroundColor = new Color(0, 0, 0, Settings.BackgroundOpacity.Value);
            Settings.BackgroundOpacity.Subscribe(this, opacity => BackgroundColor = new Color(0, 0, 0, opacity));
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

        protected override void OnMoved(MovedEventArgs e)
        {
            if (Settings.FixatedWindow.Value)
            {
                Location = new Point(Settings.WindowLocationX.Value, Settings.WindowLocationY.Value);
                return;
            }

            Settings.WindowLocationX.Value = e.CurrentLocation.X;
            Settings.WindowLocationY.Value = e.CurrentLocation.Y;
            base.OnMoved(e);
        }

        protected override void OnClick(MouseEventArgs e)
        {
            ConfirmDeletionWindow.Hide();
            base.OnClick(e);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            // hacky check to prevent infinite recursive call
            if (!Size.Equals(new Point(GetWindowRegion.Width, GetWindowRegion.Height + 40)))
                ConstructWindow(Resources.GetTexture(Textures.Empty), GetWindowRegion, GetContentRegion);
            
            Settings.WindowWidth.Value = e.CurrentSize.X;
            Settings.WindowHeight.Value = e.CurrentSize.Y - 40;
            
            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            Settings.WindowOpacityWhenNotFocussed.Unsubscribe(this);
            Settings.FixatedWindow.Unsubscribe(this);
            Settings.BackgroundOpacity.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}