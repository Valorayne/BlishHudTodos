using System;
using System.Collections.Generic;

namespace TodoList
{
    public abstract class ModuleEntity : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private static readonly List<ModuleEntity> _entities = new List<ModuleEntity>();

        protected ModuleEntity()
        {
            _entities.Add(this);
        }
        
        protected T RegisterForDisposal<T>(T disposable) where T : IDisposable
        {
            _disposables.Add(disposable);
            return disposable;
        }

        public void Dispose()
        {
            CustomDispose();
            foreach (var disposable in _disposables)
                disposable.Dispose();
            _disposables.Clear();
        }

        protected virtual void CustomDispose()
        {
            
        }

        public static void DisposeAllEntities()
        {
            foreach (var entity in _entities)
                entity.Dispose();
            _entities.Clear();
        }

        protected virtual void Initialize()
        {
            
        }

        public static void InitializeAllEntities()
        {
            foreach (var entity in _entities)
                entity.Initialize();
        }
    }
}