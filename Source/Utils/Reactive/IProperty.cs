using System;

namespace Todos.Source.Utils.Reactive
{
    public interface IProperty
    {
        void Unsubscribe(object subscriber);
    }
    
    public interface IProperty<out T> : IDisposable, IProperty
    {
        T Value { get; }

        IProperty<T> Subscribe(object subscriber, Action<T> handler, bool executeImmediately = true);
        IProperty<T> Subscribe(object subscriber, Action<T, T> handler, bool executeImmediately = true);
    }
}