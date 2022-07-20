using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;

namespace Todos.Source.Utils.Subscriptions
{
    public class HoverSubscription : IDisposable
    {
        private readonly Panel _reference;
        private readonly Action _onMouseEntered;
        private readonly Action _onMouseLeft;

        public HoverSubscription(Panel reference, Action onMouseEntered, Action onMouseLeft)
        {
            _reference = reference;
            _onMouseEntered = onMouseEntered;
            _onMouseLeft = onMouseLeft;

            _reference.MouseEntered += OnMouseEntered;
            _reference.MouseLeft += OnMouseLeft;
        }

        private void OnMouseLeft(object sender, MouseEventArgs e) => _onMouseLeft();
        private void OnMouseEntered(object sender, MouseEventArgs e) => _onMouseEntered();

        public void Dispose()
        {
            _reference.MouseEntered -= OnMouseEntered;
            _reference.MouseLeft -= OnMouseLeft;
        }
    }
}