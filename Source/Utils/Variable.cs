using System;
using Blish_HUD.Settings;

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
        private Action _onDisposal;

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

        public Variable(object owner, SettingEntry<T> setting)
        {
            _owner = owner;
            _value = setting.Value;
            _onChange = newValue => setting.Value = newValue;
        }

        public static Variable<T> Combine<A, B>(Variable<A> a, Variable<B> b, Func<A, B, T> combiner)
        {
            var result = new Variable<T>(a._owner, combiner(a.Value, b.Value));

            var aHandler = new Variable<A>.ValueChangedEvent(newA => result.Value = combiner(newA, b.Value));
            a.Changed += aHandler;
            
            var bHandler = new Variable<B>.ValueChangedEvent(newB => result.Value = combiner(a.Value, newB));
            b.Changed += bHandler;
            
            result._onDisposal = () =>
            {
                a.Changed -= aHandler;
                b.Changed -= bHandler;
            };

            return result;
        }
        
        public static Variable<R> Combine<A, B, C, R>(Variable<A> a, Variable<B> b, Variable<C> c, Func<A, B, C, R> combiner)
        {
            var result = new Variable<R>(a._owner, combiner(a.Value, b.Value, c.Value));

            var aHandler = new Variable<A>.ValueChangedEvent(newA => result.Value = combiner(newA, b.Value, c.Value));
            a.Changed += aHandler;
            
            var bHandler = new Variable<B>.ValueChangedEvent(newB => result.Value = combiner(a.Value, newB, c.Value));
            b.Changed += bHandler;
            
            var cHandler = new Variable<C>.ValueChangedEvent(newC => result.Value = combiner(a.Value, b.Value, newC));
            c.Changed += cHandler;
            
            result._onDisposal = () =>
            {
                a.Changed -= aHandler;
                b.Changed -= bHandler;
                c.Changed -= cHandler;
            };

            return result;
        }

        public void Dispose()
        {
            _onDisposal?.Invoke();
            _onDisposal = null;
            
            _owner = null;
            _value = default;
            _onChange = null;
            Changed = null;
            PropertyChanged = null;
        }
    }
}