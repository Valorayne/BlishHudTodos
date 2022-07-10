using System;
using Microsoft.Xna.Framework;

namespace Todos.Source.Utils
{
    public static class TimeService
    {
        private static DateTime? _lastMinute;

        public static event EventHandler<GameTime> NewMinute;

        public static void ProgressTimer(GameTime time)
        {
            if (time.IsRunningSlowly)
                return;

            if (!_lastMinute.HasValue || (DateTime.Now - _lastMinute.Value).TotalMinutes > 1)
            {
                _lastMinute = DateTime.Now.WithoutSeconds();
                NewMinute?.Invoke(time, time);
            }
        }

        public static void Dispose()
        {
            _lastMinute = null;
        }
    }
}