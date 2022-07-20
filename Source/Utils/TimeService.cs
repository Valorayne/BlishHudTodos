using System;
using Microsoft.Xna.Framework;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Utils
{
    public static class TimeService
    {
        public static Variable<DateTime> NewMinute = new Variable<DateTime>(DateTime.Now.WithoutSeconds());

        public static void ProgressTimer(GameTime time)
        {
            if (!time.IsRunningSlowly) 
                NewMinute.Value = DateTime.Now.WithoutSeconds();
        }

        public static void Dispose()
        {
            NewMinute.Dispose();
            NewMinute = new Variable<DateTime>(DateTime.Now.WithoutSeconds());
        }
    }
}