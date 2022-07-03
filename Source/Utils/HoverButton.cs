using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TodoList
{
    public class HoverButton : Image
    {
        private readonly Texture2D _normal;
        private readonly Texture2D _hovered;
        private readonly Action<MouseEventArgs> _onClick;

        public HoverButton(Texture2D normal, Texture2D hovered, int width, int height, Action<MouseEventArgs> onClick) : base(normal)
        {
            _normal = normal;
            _hovered = hovered;
            _onClick = onClick;
            
            Width = width;
            Height = height;

            MouseEntered += OnMouseEntered;
            MouseLeft += OnMouseLeft;
            Click += OnClick;
        }

        private void OnMouseLeft(object sender, MouseEventArgs e)
        {
            Texture = _normal;
        }

        private void OnMouseEntered(object sender, MouseEventArgs e)
        {
            Texture = _hovered;
        }

        private void OnClick(object target, MouseEventArgs args)
        {
            _onClick(args);
        }

        protected override void DisposeControl()
        {
            MouseEntered -= OnMouseEntered;
            MouseLeft -= OnMouseLeft;
            Click -= OnClick;
            base.DisposeControl();
        }
    }
}