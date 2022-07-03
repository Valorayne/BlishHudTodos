using System.Collections.Generic;
using TodoList.Models;

namespace TodoList
{
    public static class Data
    {
        public static IReadOnlyList<Todo> Todos { get; private set; }

        public static void Initialize()
        {
            var todos = new List<Todo>();

            for (var i = 0; i < 5; i++)
            {
                todos.Add(new Todo("Some test content with super long text"));
            }

            Todos = todos;
        }

        public static void Dispose()
        {
            (Todos as List<Todo>)?.Clear();
            Todos = null;
        }
    }
}