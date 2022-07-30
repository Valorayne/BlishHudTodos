using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;

namespace Todos.Source.Components.Messages
{
    public sealed class ConfirmDeletionWindow : FlowPanel
    {
        private const int PADDING = 5;
        private const int BUTTON_WIDTH = 50;
        public const int WIDTH = 2 * BUTTON_WIDTH + 2 * PADDING;

        public Action OnYes;
        public Action OnNo;

        private readonly StandardButton _yes;
        private readonly StandardButton _no;

        public ConfirmDeletionWindow()
        {
            Parent = GameService.Graphics.SpriteScreen;
            HeightSizingMode = SizingMode.AutoSize;
            Width = WIDTH;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            Title = "Are you sure?";
            ZIndex = Screen.TOOLTIP_BASEZINDEX;
            OuterControlPadding = Vector2.One * PADDING;

            _yes = new StandardButton { Parent = this, Text = "Yes", Width = BUTTON_WIDTH };
            _no = new StandardButton { Parent = this, Text = "No", Width = BUTTON_WIDTH };

            _yes.Click += OnYesHandler;
            _no.Click += OnNoHandler;
        }

        private void OnNoHandler(object sender, MouseEventArgs e) => OnNo?.Invoke();
        private void OnYesHandler(object sender, MouseEventArgs e) => OnYes?.Invoke();

        protected override void DisposeControl()
        {
            _yes.Click -= OnYesHandler;
            _no.Click -= OnNoHandler;
            OnYes = null;
            OnNo = null;
            base.DisposeControl();
        }
    }
}