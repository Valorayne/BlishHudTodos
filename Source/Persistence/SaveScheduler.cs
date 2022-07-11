using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Blish_HUD.Modules.Managers;
using Microsoft.Xna.Framework;

namespace Todos.Source.Persistence
{
    public static class SaveScheduler
    {
        private static readonly TimeSpan INTERVAL = TimeSpan.FromSeconds(1);
        
        private static Persistence _persistence;
        private static TimeSpan? _lastSaveProcess;
        private static ConcurrentDictionary<long, TodoJson> _changedTodos; 

        public static void Initialize(DirectoriesManager manager)
        {
            _persistence = new Persistence(manager);
            _changedTodos = new ConcurrentDictionary<long, TodoJson>();
        }

        public static void MarkAsChanged(TodoJson todo)
        {
            _changedTodos[todo.CreatedAt.Ticks] = todo;
        }

        public static void Progress(GameTime time)
        {
            if (_changedTodos.Count > 0)
            {
                if (!_lastSaveProcess.HasValue || time.TotalGameTime >= _lastSaveProcess.Value + INTERVAL)
                {
                    _lastSaveProcess = time.TotalGameTime;
                    PersistAll();
                }
            }
        }

        private static void PersistAll()
        {
            Task.WaitAll(_changedTodos.Select(entry => Task.Run(() => 
            {
                if (_changedTodos.TryRemove(entry.Key, out var todo) )
                    _persistence.Persist(todo);
            })).ToArray());
        }

        public static void Dispose()
        {
            if (_changedTodos.Count > 0)
                PersistAll();
            
            _changedTodos = null;
            _lastSaveProcess = null;
            _persistence = null;
        }
    }
}