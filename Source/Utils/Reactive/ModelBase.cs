using System;
using System.Collections.Generic;
using System.Linq;

namespace Todos.Source.Utils.Reactive
{
    public abstract class ModelBase : IDisposable
    {
        private readonly List<IDisposable> _properties = new List<IDisposable>();

        protected T Add<T>(T property) where T : IDisposable
        {
            _properties.Add(property);
            return property;
        }

        protected virtual void DisposeModel() { }

        public void Unsubscribe(object subscriber)
        {
            foreach (var property in _properties.OfType<IProperty>())
                property.Unsubscribe(subscriber);
        }

        public void Dispose()
        {
            DisposeModel();
            foreach (var property in _properties)
                property.Dispose();
        }
    }
}