using System;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class DailyReset : IReset
    {
        private static DateTimeOffset LastDailyReset => DateTimeOffset.UtcNow.StartOfDay();
        private static DateTimeOffset NextDailyReset => DateTimeOffset.UtcNow.StartOfDay() + TimeSpan.FromDays(1);
        public TodoScheduleType Type => TodoScheduleType.DailyServer;

        public string DropdownEntry => "Daily Server Reset";
        public string DropdownEntryTooltip => "This task will reset every day at 0:00 UTC";

        public bool IsDone(DateTimeOffset now, DateTimeOffset lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return lastExecution > LastDailyReset;
        }

        public string IconTooltip(DateTimeOffset now, DateTimeOffset? lastExecution, TimeSpan localTime,
            TimeSpan duration)
        {
            return $"Daily reset in {NextDailyReset.ToDurationString()}";
        }

        public string ClipboardContent(DateTimeOffset now)
        {
            return null;
        }
    }
}