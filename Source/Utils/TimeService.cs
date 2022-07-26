using System;
using Microsoft.Xna.Framework;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Utils
{
    public static class TimeService
    {
        public static Variable<DateTimeOffset> NewMinute = new Variable<DateTimeOffset>(DateTimeOffset.Now.WithoutSeconds());

        public static void ProgressTimer(GameTime time)
        {
            if (!time.IsRunningSlowly) 
                NewMinute.Value = DateTimeOffset.Now.WithoutSeconds();
        }

        public static void Dispose()
        {
            NewMinute.Dispose();
            NewMinute = new Variable<DateTimeOffset>(DateTimeOffset.Now.WithoutSeconds());
        }
    }
}