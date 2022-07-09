using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blish_HUD;
using Blish_HUD.Modules.Managers;
using Newtonsoft.Json;
using Todos.Source.Models;

namespace Todos.Source.Utils
{
    public static class Data
    {
        private const string FILE_ENDING = ".todo.json";
        private static readonly JsonSerializerSettings SETTINGS = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };
        
        private static Dictionary<long, Todo> _todos;
        
        public static IReadOnlyList<Todo> Todos => _todos.Values.ToList();

        public static event EventHandler<Todo> TodoAdded;
        public static event EventHandler<Todo> TodoModified;
        public static event EventHandler<Todo> TodoDeleted;
        
        private static readonly Logger Logger = Logger.GetLogger<TodoModule>();
        private static string _directoryPath;

        public static Task Initialize(DirectoriesManager manager)
        {
            _directoryPath = manager.GetFullDirectoryPath("todos");
            _todos = new Dictionary<long, Todo>();
            foreach (var file in Directory.GetFiles(_directoryPath, $"*{FILE_ENDING}"))
            {
                try
                {
                    var jsonString = File.ReadAllText(file);
                    var todo = JsonConvert.DeserializeObject<Todo>(jsonString, SETTINGS);
                    if (todo != null)
                        _todos.Add(todo.CreatedAt.Ticks, todo);
                }
                catch (Exception e)
                {
                    Logger.Error($"Could not deserialize file '{Path.Combine(_directoryPath, file)}':\r\n{e.Message}");
                }
            }
            return Task.CompletedTask;
        }

        public static void Save(Todo todo)
        {
            var filePath = GetFilePath(todo);
            try
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(todo, SETTINGS));
            }
            catch (Exception e)
            {
                Logger.Error($"Could not save file '{filePath}':\r\n{e.Message}");
            }
            TodoModified?.Invoke(todo, todo);
        }

        public static void Add(Todo todo)
        {
            _todos.Add(todo.CreatedAt.Ticks, todo);
            var filePath = GetFilePath(todo);
            try
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(todo, SETTINGS));
            }
            catch (Exception e)
            {
                Logger.Error($"Could not add file '{filePath}':\r\n{e.Message}");
            }
            TodoAdded?.Invoke(todo, todo);
        }

        private static string GetFilePath(Todo todo)
        {
            return Path.Combine(_directoryPath, $"{todo.CreatedAt.Ticks.ToString()}{FILE_ENDING}");
        }

        public static void Delete(Todo todo)
        {
            _todos.Remove(todo.CreatedAt.Ticks);
            var filePath = GetFilePath(todo);
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch (Exception e)
            {
                Logger.Error($"Could not delete file '{filePath}':\r\n{e.Message}");
            }
            TodoDeleted?.Invoke(todo, todo);
        }

        public static void Dispose()
        {
            _directoryPath = null;
            _todos?.Clear();
            _todos = null;
        }
    }
}