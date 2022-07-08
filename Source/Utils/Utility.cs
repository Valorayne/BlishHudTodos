using System;
using System.Threading.Tasks;

namespace TodoList
{
    public static class Utility
    {
        public static void Delay(Action action, int ms = 1)
        {
            Task.Run(async () =>
            {
                await Task.Delay(ms);
                action();
            });
        }
    }
}