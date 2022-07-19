using System;

namespace Todos.Source.Utils
{
    public interface IVariable<T> : IProperty<T>
    {
        new T Value { get; set; }
        new IVariable<T> Subscribe(object subscriber, Action<T> handler, bool executeImmediately = true);
        new IVariable<T> Subscribe(object subscriber, Action<T, T> handler, bool executeImmediately = true);
    }
}