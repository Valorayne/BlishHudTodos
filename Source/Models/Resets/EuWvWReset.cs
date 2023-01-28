using System;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class EuWvWReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.EuWvW;

        public string DropdownEntry => "EU World vs World Reset";
        public string DropdownEntryTooltip => "This task will reset every Friday, 18:00 UTC";

        public bool IsDone(DateTimeOffset now, DateTimeOffset lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return lastExecution > LastWeeklyReset(now);
        }

        public string IconTooltip(DateTimeOffset now, DateTimeOffset? lastExecution, TimeSpan localTime,
            TimeSpan duration)
        {
            return $"EU World vs World reset in {NextWeeklyReset(now).ToDurationString()}";
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
            while (date.DayOfWeek != DayOfWeek.Friday)
                date += TimeSpan.FromDays(1);
            var time = date + TimeSpan.FromHours(18);
            return now > time ? time + TimeSpan.FromDays(7) : time;
        }
    }
}