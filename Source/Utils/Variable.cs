using System;

namespace Todos.Source.Utils
{
    public class Variable<T> : IDisposable
    {
        public delegate void ValueChangedEvent(T newValue);
        public event ValueChangedEvent Changed;

        public delegate void PropertyChangedEvent(object owner, T newValue);
        public event PropertyChangedEvent PropertyChanged;

        private object _owner;
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
                    PropertyChanged?.Invoke(_owner, value);
                }
            }
        }

        public Variable(object owner, T defaultValue, Action<T> propagateChange = null, Action persist = null)
        {
            _owner = owner;
            _value = defaultValue;
            _onChange = newValue =>
            {
                propagateChange?.Invoke(newValue);
                persist?.Invoke();
            };
        }

        public void Dispose()
        {
            _owner = null;
            _value = default;
            _onChange = null;
            Changed = null;
            PropertyChanged = null;
        }
    }
}