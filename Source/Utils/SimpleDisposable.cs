using System;

namespace Todos.Source.Utils
{
    public class SimpleDisposable : IDisposable
    {
        private Action _onDisposal;

        public SimpleDisposable(Action onDisposal)
        {
            _onDisposal = onDisposal;
        }

        public void Dispose()
        {
            if (_onDisposal == null)
                return;

            _onDisposal();
            _onDisposal = null;
        }
    }
}