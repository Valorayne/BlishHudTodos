using System;
using System.Collections.Generic;
using System.Linq;

namespace Todos.Source.Utils.Reactive
{
    public class Variable<T> : IVariable<T>
    {
        private IDictionary<object, Action<T, T>> _handlers = new Dictionary<object, Action<T, T>>();
        private T _value;

        public Action OnDisposal;

        public Variable(T defaultValue)
        {
            _value = defaultValue;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    var previousValue = _value;
                    _value = value;
                    foreach (var handler in _handlers.Values.ToList())
                        handler(previousValue, value);
                }
            }
        }

        public IVariable<T> Subscribe(object subscriber, Action<T, T> handler, bool executeImmediately = true)
        {
            if (!_handlers.ContainsKey(subscriber))
                _handlers[subscriber] = handler;
            else
            {
                var previousHandler = _handlers[subscriber];
                _handlers[subscriber] = (before, after) =>
                {
                    previousHandler(before, after);
                    handler(before, after);
                };
            }

            if (executeImmediately)
                handler(_value, _value);

            return this;
        }

        IProperty<T> IProperty<T>.Subscribe(object subscriber, Action<T> handler, bool executeImmediately)
        {
            return Subscribe(subscriber, handler, executeImmediately);
        }

        IProperty<T> IProperty<T>.Subscribe(object subscriber, Action<T, T> handler, bool executeImmediately)
        {
            return Subscribe(subscriber, handler, executeImmediately);
        }

        public IVariable<T> Subscribe(object subscriber, Action<T> handler, bool executeImmediately = true)
        {
            return Subscribe(subscriber, (before, after) => handler(after), executeImmediately);
        }

        public void Unsubscribe(object subscriber) => _handlers?.Remove(subscriber);

        public void Dispose()
        {
            OnDisposal?.Invoke();
            _handlers.Clear();
            _handlers = null;
            _value = default;
        }
    }
}