using System;
using Blish_HUD;
using Blish_HUD.Settings;

namespace Todos.Source.Utils
{
    public static class Variables
    {
        public static IVariable<T> Transient<T>(T defaultValue)
        {
            return new Variable<T>(defaultValue);
        }
        
        public static IVariable<T> Persistent<T>(T defaultValue, Action<T> update, Action persist)
        {
            var result = new Variable<T>(defaultValue);
            result.Subscribe(result, value =>
            {
                update(value);
                persist();
            }, false);
            return result;
        }

        public static IVariable<T> ToVariable<T>(this SettingEntry<T> setting)
        {
            var result = new Variable<T>(setting.Value);
            
            result.Subscribe(setting, value => setting.Value = value);
            var handler = new EventHandler<ValueChangedEventArgs<T>>(((sender, change) => result.Value = change.NewValue));
            setting.SettingChanged += handler;
            
            result.OnDisposal = () => setting.SettingChanged -= handler;
            return result;
        }
        
        public static IProperty<T> Select<A, T>(this IProperty<A> origin, Func<A, T> mapper)
        {
            var result = new Variable<T>(mapper(origin.Value));
            origin.Subscribe(result, value => result.Value = mapper(value));
            result.OnDisposal = () => origin.Unsubscribe(result);
            return result;
        }
        
        public static IProperty<T> CombineWith<A, B, T>(this IProperty<A> a, IProperty<B> b, Func<A, B, T> mapper)
        {
            var result = new Variable<T>(mapper(a.Value, b.Value));
            a.Subscribe(result, newA => result.Value = mapper(newA, b.Value));
            b.Subscribe(result, newB => result.Value = mapper(a.Value, newB));
            result.OnDisposal = () =>
            {
                a.Unsubscribe(result);
                b.Unsubscribe(result);
            };
            return result;
        }
        
        public static IProperty<T> CombineWith<A, B, C, T>(this IProperty<A> a, IProperty<B> b, IProperty<C> c, Func<A, B, C, T> mapper)
        {
            var result = new Variable<T>(mapper(a.Value, b.Value, c.Value));
            a.Subscribe(result, newA => result.Value = mapper(newA, b.Value, c.Value));
            b.Subscribe(result, newB => result.Value = mapper(a.Value, newB, c.Value));
            c.Subscribe(result, newC => result.Value = mapper(a.Value, b.Value, newC));
            result.OnDisposal = () =>
            {
                a.Unsubscribe(result);
                b.Unsubscribe(result);
                c.Unsubscribe(result);
            };
            return result;
        }
        
        public static IProperty<T> CombineWith<A, B, C, D, T>(this IProperty<A> a, IProperty<B> b, IProperty<C> c, IProperty<D> d, Func<A, B, C, D, T> mapper)
        {
            var result = new Variable<T>(mapper(a.Value, b.Value, c.Value, d.Value));
            a.Subscribe(result, newA => result.Value = mapper(newA, b.Value, c.Value, d.Value));
            b.Subscribe(result, newB => result.Value = mapper(a.Value, newB, c.Value, d.Value));
            c.Subscribe(result, newC => result.Value = mapper(a.Value, b.Value, newC, d.Value));
            d.Subscribe(result, newD => result.Value = mapper(a.Value, b.Value, c.Value, newD));
            result.OnDisposal = () =>
            {
                a.Unsubscribe(result);
                b.Unsubscribe(result);
                c.Unsubscribe(result);
                d.Unsubscribe(result);
            };
            return result;
        }
        
        public static IProperty<T> CombineWith<A, B, C, D, E, T>(this IProperty<A> a, IProperty<B> b, IProperty<C> c, IProperty<D> d, IProperty<E> e, Func<A, B, C, D, E, T> mapper)
        {
            var result = new Variable<T>(mapper(a.Value, b.Value, c.Value, d.Value, e.Value));
            a.Subscribe(result, newA => result.Value = mapper(newA, b.Value, c.Value, d.Value, e.Value));
            b.Subscribe(result, newB => result.Value = mapper(a.Value, newB, c.Value, d.Value, e.Value));
            c.Subscribe(result, newC => result.Value = mapper(a.Value, b.Value, newC, d.Value, e.Value));
            d.Subscribe(result, newD => result.Value = mapper(a.Value, b.Value, c.Value, newD, e.Value));
            e.Subscribe(result, newE => result.Value = mapper(a.Value, b.Value, c.Value, d.Value, newE));
            result.OnDisposal = () =>
            {
                a.Unsubscribe(result);
                b.Unsubscribe(result);
                c.Unsubscribe(result);
                d.Unsubscribe(result);
                e.Unsubscribe(result);
            };
            return result;
        }
    }
}