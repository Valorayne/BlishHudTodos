using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Utils;

namespace Todos.Source.Components.Messages
{
    public sealed class ConfirmDeletionWindow : FlowPanel
    {
        private const int PADDING = 5;
        private static ConfirmDeletionWindow _instance = new ConfirmDeletionWindow();

        private Action _action;
        
        private readonly StandardButton _yes;
        private readonly StandardButton _no;

        public static void Spawn(Point location, Action action)
        {
            _instance._action = action;
            _instance.Show();
            Utility.Delay(() => _instance.Location = new Point(location.X - _instance.Width, location.Y));
        }

        public ConfirmDeletionWindow()
        {
            Parent = GameService.Graphics.SpriteScreen;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            Title = "Are you sure?";
            ZIndex = Screen.TOOLTIP_BASEZINDEX;
            OuterControlPadding = Vector2.One * PADDING; 
            
            _yes = new StandardButton { Parent = this, Text = "Yes", Width = 50 };
            _no = new StandardButton { Parent = this, Text = "No", Width = 50 };

            _yes.Click += OnYes;
            _no.Click += OnNo;
        }

        private static void OnNo(object sender, MouseEventArgs e)
        {
            Hide();
        }

        private void OnYes(object sender, MouseEventArgs e)
        {
            _action();
            Hide();
        }

        protected override void DisposeControl()
        {
            _yes.Click -= OnYes;
            _no.Click -= OnNo;
            base.DisposeControl();
        }

        public new static void Hide()
        {
            if (_instance.Visible)
                ((Control)_instance).Hide();
        } 

        public new static void Dispose()
        {
            ((Control)_instance).Dispose();
            _instance = null;
        }
    }
}