using System;

namespace Todos.Source.Utils
{
    public class Variable<T> : IDisposable
    {
        public delegate void ValueChangedEvent(T newValue);
        public event ValueChangedEvent Changed;
        
        private T _value;
        private Action<T> _onChange;

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(value, _value))
                {
                    _value = value;
                    _onChange(value);
                    Changed?.Invoke(value);
                }
            }
        }

        public Variable(T defaultValue, Action<T> propagateChange, Action persist)
        {
            _value = defaultValue;
            _onChange = newValue =>
            {
                propagateChange(newValue);
                persist();
            };
        }

        public void Dispose()
        {
            _value = default;
            _onChange = null;
            Changed = null;
        }
    }
}