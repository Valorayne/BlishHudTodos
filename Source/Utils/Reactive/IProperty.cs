using System;

namespace Todos.Source.Utils
{
    public interface IProperty<out T> : IDisposable
    {
        T Value { get; }
        
        IProperty<T> Subscribe(object subscriber, Action<T> handler, bool executeImmediately = true);
        IProperty<T> Subscribe(object subscriber, Action<T, T> handler, bool executeImmediately = true);
        void Unsubscribe(object subscriber);
    }
}