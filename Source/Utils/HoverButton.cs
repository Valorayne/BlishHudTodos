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
        }

        protected override void OnMouseLeft(MouseEventArgs e)
        {
            Texture = _normal;
            base.OnMouseLeft(e);
        }

        protected override void OnMouseEntered(MouseEventArgs e)
        {
            Texture = _hovered;
            base.OnMouseEntered(e);
        }

        protected override void OnClick(MouseEventArgs e)
        {
            _onClick(e);
            base.OnClick(e);
        }
    }
}