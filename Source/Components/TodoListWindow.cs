using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
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
        private readonly TodoListPanel _panel;
        private readonly PopupModel _popup;
        private readonly SettingsModel _settings;

        private bool _hovered;

        public TodoListWindow(SettingsModel settings, TodoListModel todoList, PopupModel popup)
            : base(Resources.GetTexture(Textures.Empty), GetWindowRegion(settings), GetContentRegion(settings))
        {
            _settings = settings;
            _popup = popup;
            _panel = new TodoListPanel(settings, todoList, popup) { Parent = this };

            Parent = GameService.Graphics.SpriteScreen;
            CanResize = !settings.FixatedWindow.Value;
            CanClose = false;
            Opacity = settings.WindowOpacityWhenNotFocussed.Value;
            Location = new Point(settings.WindowLocationX.Value, settings.WindowLocationY.Value);
            Visible = true;

            settings.WindowTitle.Subscribe(this, title => Title = title);
            settings.WindowOpacityWhenNotFocussed.Subscribe(this, newOpacity =>
            {
                if (!_hovered) Opacity = newOpacity;
            });
            settings.FixatedWindow.Subscribe(this, fixated => CanResize = !fixated);
            settings.BackgroundOpacity.Subscribe(this, opacity => BackgroundColor = new Color(0, 0, 0, opacity));
        }

        private static Rectangle GetWindowRegion(SettingsModel settings)
        {
            return new Rectangle(0, -28,
                Math.Max(MIN_WIDTH, Math.Min(MAX_WIDTH, settings.WindowWidth.Value)),
                Math.Max(MIN_HEIGHT, Math.Min(MAX_HEIGHT, settings.WindowHeight.Value)));
        }

        private static Rectangle GetContentRegion(SettingsModel settings)
        {
            return new Rectangle(0, -28,
                GetWindowRegion(settings).Width, GetWindowRegion(settings).Height);
        }

        protected override CaptureType CapturesInput()
        {
            return base.CapturesInput() | CaptureType.DoNotBlock;
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
            Opacity = _settings.WindowOpacityWhenNotFocussed.Value;
            base.OnMouseLeft(e);
        }

        protected override void OnMoved(MovedEventArgs e)
        {
            _popup?.Close();

            if (_settings.FixatedWindow.Value)
            {
                Location = new Point(_settings.WindowLocationX.Value, _settings.WindowLocationY.Value);
                return;
            }

            _settings.WindowLocationX.Value = e.CurrentLocation.X;
            _settings.WindowLocationY.Value = e.CurrentLocation.Y;
            base.OnMoved(e);
        }

        protected override void OnClick(MouseEventArgs e)
        {
            _popup.Close();
            base.OnClick(e);
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            _popup?.Close();
            _panel?.SaveScroll();

            if (_settings == null)
            {
                base.OnResized(e);
                return;
            }

            // hacky check to prevent infinite recursive call
            if (!Size.Equals(new Point(GetWindowRegion(_settings).Width, GetWindowRegion(_settings).Height + 40)))
                ConstructWindow(Resources.GetTexture(Textures.Empty), GetWindowRegion(_settings),
                    GetContentRegion(_settings));

            _settings.WindowWidth.Value = e.CurrentSize.X;
            _settings.WindowHeight.Value = e.CurrentSize.Y - 40;

            base.OnResized(e);
        }

        protected override void DisposeControl()
        {
            _settings.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}