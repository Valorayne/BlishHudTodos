using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Utils;

// ReSharper disable VirtualMemberCallInConstructor

namespace Todos.Source.Components.Generic
{
    public abstract class CenteredMessage : Panel
    {
        private const int HEIGHT = 100;
        private const int WIDTH = 250;

        private readonly Panel _panel;

        protected CenteredMessage(string text, Point labelLocation)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;

            _panel = new Panel
            {
                Parent = this,
                Width = WIDTH,
                Height = HEIGHT,
                BackgroundTexture = Resources.GetTexture(Textures.Header)
            };

            new Label
            {
                Parent = _panel,
                Text = text,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                StrokeText = true,
                Height = HEIGHT,
                Width = WIDTH,
                Location = labelLocation
            };
        }

        protected override void OnResized(ResizedEventArgs e)
        {
            if (_panel != null)
                _panel.Location = new Point((Width - _panel.Width) / 2, (Height - _panel.Height) / 2);

            base.OnResized(e);
        }
    }
}