using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blish_HUD.Modules.Managers;
using Microsoft.Xna.Framework;
using Todos.Source.Models;

namespace Todos.Source.Utils
{
    public static class SaveScheduler
    {
        private static readonly TimeSpan INTERVAL = TimeSpan.FromSeconds(1);
        
        private static Persistence _persistence;
        private static TimeSpan? _lastSaveProcess;
        private static ConcurrentBag<Todo> _changedTodos; 

        public static void Initialize(DirectoriesManager manager)
        {
            _persistence = new Persistence(manager);
            _changedTodos = new ConcurrentBag<Todo>();
            
            Data.TodoAdded += OnTodoChanged;
            Data.TodoModified += OnTodoChanged;
            Data.TodoDeleted += OnTodoChanged;
        }

        private static void OnTodoChanged(object sender, Todo todo)
        {
            _changedTodos.Add(todo);
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
            var persistenceTasks = new List<Task>();
            while (!_changedTodos.IsEmpty)
            {
                persistenceTasks.Add(Task.Run(() =>
                {
                    if (_changedTodos.TryTake(out var todo))
                        _persistence.Persist(todo);
                }));
            }
            Task.WaitAll(persistenceTasks.ToArray());
        }

        public static void Dispose()
        {
            Data.TodoAdded -= OnTodoChanged;
            Data.TodoModified -= OnTodoChanged;
            Data.TodoDeleted -= OnTodoChanged;

            
            _changedTodos = null;
            _lastSaveProcess = null;
            _persistence = null;
        }
    }
}