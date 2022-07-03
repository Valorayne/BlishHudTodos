using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public static class TodoDetailsWindowPool
    {
        private static TodoDetailsWindow _instance;
        
        public static void Spawn(Point location, Todo existingTodo = null)
        {
            _instance?.Dispose();
            _instance = TodoDetailsWindow.Create(new Point(location.X - TodoDetailsWindow.WIDTH, location.Y), existingTodo);
            _instance.Show();
        }
        
        public static void Dispose()
        {
            _instance?.Dispose();
            _instance = null;
        }
    }
}