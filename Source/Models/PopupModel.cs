using System;
using Blish_HUD.Controls;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Models
{
    public class PopupModel : ModelBase
    {
        public Container Parent { get; private set; }
        private IDisposable _openPopup;

        public T Open<T>(Container parent, T popup) where T : IDisposable
        {
            Parent = parent;
            _openPopup?.Dispose();
            _openPopup = popup;
            return popup;
        }

        public void Close()
        {
            _openPopup?.Dispose();
            _openPopup = null;
            Parent = null;
        }

        protected override void DisposeModel()
        {
            Close();
            base.DisposeModel();
        }
    }
}