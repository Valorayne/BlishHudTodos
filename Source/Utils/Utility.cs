using System;
using System.Threading.Tasks;

namespace TodoList
{
    public static class Utility
    {
        public static void NextFrame(Action action)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1);
                action();
            });
        }
    }
}