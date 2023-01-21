using System;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class WeeklyReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.WeeklyServer;

        public string DropdownEntry => "Weekly Server Reset";
        public string DropdownEntryTooltip => "This task will reset every Monday, 7:30 UTC";

        public bool IsDone(DateTimeOffset now, DateTimeOffset lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return lastExecution > LastWeeklyReset(now);
        }

        public string IconTooltip(DateTimeOffset now, DateTimeOffset? lastExecution, TimeSpan localTime,
            TimeSpan duration)
        {
            return $"Weekly reset in {NextWeeklyReset(now).ToDurationString()}";
        }

        public string ClipboardContent(DateTimeOffset now)
        {
            return null;
        }

        private static DateTimeOffset LastWeeklyReset(DateTimeOffset now)
        {
            return NextWeeklyReset(now) - TimeSpan.FromDays(7);
        }

        private static DateTimeOffset NextWeeklyReset(DateTimeOffset now)
        {
            var date = DateTimeOffset.UtcNow.StartOfDay();
            while (date.DayOfWeek != DayOfWeek.Monday)
                date += TimeSpan.FromDays(1);
            var time = date + TimeSpan.FromHours(7) + TimeSpan.FromMinutes(30);
            return now > time ? time + TimeSpan.FromDays(7) : time;
        }
    }
}