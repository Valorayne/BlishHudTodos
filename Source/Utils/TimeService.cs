using System;
using Microsoft.Xna.Framework;

namespace TodoList
{
    public static class TimeService
    {
        private static TimeSpan? _lastMinuteTick;

        public static event EventHandler<GameTime> NewMinute;

        public static void ProgressTimer(GameTime time)
        {
            if (time.IsRunningSlowly)
                return;

            if (!_lastMinuteTick.HasValue || (time.TotalGameTime - _lastMinuteTick.Value).TotalMinutes > 1)
            {
                _lastMinuteTick = time.TotalGameTime;
                NewMinute?.Invoke(time, time);
            }
        }

        public static void Dispose()
        {
            _lastMinuteTick = null;
        }
    }
}