using System;
using System.Collections.Generic;
using System.Linq;
using Blish_HUD;
using Blish_HUD.Settings;

namespace Todos.Source.Utils.Reactive
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

        public static IProperty<T> FromEvent<T>(T startValue, Action<EventHandler<T>> subscribe,
            Action<EventHandler<T>> unsubscribe)
        {
            var result = new Variable<T>(startValue);
            var eventHandler = new EventHandler<T>((sender, newValue) => result.Value = newValue);
            subscribe(eventHandler);
            result.OnDisposal = () => unsubscribe(eventHandler);
            return result;
        }

        public static IProperty<T> FromEvent<T>(T startValue, Action<EventHandler<ValueEventArgs<T>>> subscribe,
            Action<EventHandler<ValueEventArgs<T>>> unsubscribe)
        {
            var result = new Variable<T>(startValue);
            var eventHandler = new EventHandler<ValueEventArgs<T>>((sender, e) => result.Value = e.Value);
            subscribe(eventHandler);
            result.OnDisposal = () => unsubscribe(eventHandler);
            return result;
        }

        public static IVariable<T> ToVariable<T>(this SettingEntry<T> setting)
        {
            var result = new Variable<T>(setting.Value);

            result.Subscribe(setting, value => setting.Value = value);
            var handler =
                new EventHandler<ValueChangedEventArgs<T>>((sender, change) => result.Value = change.NewValue);
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
            a.Subscribe(result, newA => result.Value = mapper(newA, b.Value), false);
            b.Subscribe(result, newB => result.Value = mapper(a.Value, newB));
            result.OnDisposal = () =>
            {
                a.Unsubscribe(result);
                b.Unsubscribe(result);
            };
            return result;
        }

        public static IProperty<T> CombineWith<A, B, C, T>(this IProperty<A> a, IProperty<B> b, IProperty<C> c,
            Func<A, B, C, T> mapper)
        {
            var result = new Variable<T>(mapper(a.Value, b.Value, c.Value));
            a.Subscribe(result, newA => result.Value = mapper(newA, b.Value, c.Value), false);
            b.Subscribe(result, newB => result.Value = mapper(a.Value, newB, c.Value), false);
            c.Subscribe(result, newC => result.Value = mapper(a.Value, b.Value, newC));
            result.OnDisposal = () =>
            {
                a.Unsubscribe(result);
                b.Unsubscribe(result);
                c.Unsubscribe(result);
            };
            return result;
        }

        public static IProperty<T> CombineWith<A, B, C, D, T>(this IProperty<A> a, IProperty<B> b, IProperty<C> c,
            IProperty<D> d, Func<A, B, C, D, T> mapper)
        {
            var result = new Variable<T>(mapper(a.Value, b.Value, c.Value, d.Value));
            a.Subscribe(result, newA => result.Value = mapper(newA, b.Value, c.Value, d.Value), false);
            b.Subscribe(result, newB => result.Value = mapper(a.Value, newB, c.Value, d.Value), false);
            c.Subscribe(result, newC => result.Value = mapper(a.Value, b.Value, newC, d.Value), false);
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

        public static IProperty<T> CombineWith<A, B, C, D, E, T>(this IProperty<A> a, IProperty<B> b, IProperty<C> c,
            IProperty<D> d, IProperty<E> e, Func<A, B, C, D, E, T> mapper)
        {
            var result = new Variable<T>(mapper(a.Value, b.Value, c.Value, d.Value, e.Value));
            a.Subscribe(result, newA => result.Value = mapper(newA, b.Value, c.Value, d.Value, e.Value), false);
            b.Subscribe(result, newB => result.Value = mapper(a.Value, newB, c.Value, d.Value, e.Value), false);
            c.Subscribe(result, newC => result.Value = mapper(a.Value, b.Value, newC, d.Value, e.Value), false);
            d.Subscribe(result, newD => result.Value = mapper(a.Value, b.Value, c.Value, newD, e.Value), false);
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

        public static void Add<T>(this IVariable<List<T>> variable, T element)
        {
            variable.Value = variable.Value.Append(element).ToList();
        }

        public static void Remove<T>(this IVariable<List<T>> variable, T element)
        {
            variable.Value = variable.Value.Where(v => !EqualityComparer<T>.Default.Equals(v, element)).ToList();
        }

        public static void OrderBy<T>(this IVariable<List<T>> variable, Func<T, long> sorter)
        {
            variable.Value = variable.Value.OrderBy(sorter).ToList();
        }

        public static void Toggle(this IVariable<bool> variable)
        {
            variable.Value = !variable.Value;
        }

        public static T Reset<T>(this IVariable<T> variable) where T : class
        {
            var before = variable.Value;
            variable.Value = null;
            return before;
        }

        public static void Set<A, B>(this IVariable<Tuple<A, B>> variable, A a, B b)
        {
            variable.Value = new Tuple<A, B>(a, b);
        }
    }
}