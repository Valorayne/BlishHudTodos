using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blish_HUD;
using Blish_HUD.Modules.Managers;
using Newtonsoft.Json;
using Todos.Source.Models;
using Todos.Source.Persistence.Migrations;

namespace Todos.Source.Persistence
{
    public class Persistence
    {
        private const string FILE_ENDING = ".todo.json";
        private static readonly Logger Logger = Logger.GetLogger<TodoModule>();
        
        private static readonly JsonSerializerSettings SETTINGS = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };
        
        private readonly string _directoryPath;
        
        public Persistence(DirectoriesManager manager)
        {
            _directoryPath = manager.GetFullDirectoryPath("todos");
        }

        public Task<List<TodoModel>> LoadAll()
        {
            var files = Directory.GetFiles(_directoryPath, $"*{FILE_ENDING}");
            var todos = new ConcurrentBag<TodoJson>();
            var tasks = files.Select(filePath => Task.Run(() =>
            {
                Try(filePath, "deserialize", file =>
                {
                    var jsonString = File.ReadAllText(file);
                    var migrated = Migrator.Migrate(jsonString);
                    var json = JsonConvert.DeserializeObject<TodoJson>(migrated, SETTINGS);
                    if (json != null)
                        todos.Add(json);
                });
            }));
            Task.WaitAll(tasks.ToArray());
            return Task.FromResult(todos.Select(json => new TodoModel(json, false)).ToList());
        }

        public void Persist(TodoJson todo)
        {
            if (todo.IsDeleted)
                Delete(todo); 
            else Override(todo);
        }
        
        private void Override(TodoJson todo)
        {
            var path = GetFilePath(todo);
            Try(path, File.Exists(path) ? "save" : "add", 
                filePath => File.WriteAllText(filePath, JsonConvert.SerializeObject(todo, SETTINGS)));
        }

        private void Delete(TodoJson todo)
        {
            Try(GetFilePath(todo), "delete", filePath =>
            {
                if (File.Exists(filePath)) 
                    File.Delete(filePath);
            });
        }
        
        private static void Try(string filePath, string operation, Action<string> action)
        {
            try { action(filePath); }
            catch (Exception e) { Logger.Error($"Could not {operation} file '{filePath}':\r\n{e.Message}"); }
        }
        
        private string GetFilePath(TodoJson todo)
        {
            return Path.Combine(_directoryPath, $"{todo.CreatedAt.Ticks.ToString()}{FILE_ENDING}");
        }
    }
}