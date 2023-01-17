using System;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class NaWvWReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.NaWvW;

        public string DropdownEntry => "NA World vs World Reset";
        public string DropdownEntryTooltip => "This task will reset every Saturday, 02:00 UTC";

        public bool IsDone(DateTimeOffset now, DateTimeOffset lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return lastExecution > LastWeeklyReset(now);
        }

        public string IconTooltip(DateTimeOffset now, DateTimeOffset? lastExecution, TimeSpan localTime,
            TimeSpan duration)
        {
            return $"NA World vs World reset in {NextWeeklyReset(now).ToDurationString()}";
        }

        private static DateTimeOffset LastWeeklyReset(DateTimeOffset now)
        {
            return NextWeeklyReset(now) - TimeSpan.FromDays(7);
        }

        private static DateTimeOffset NextWeeklyReset(DateTimeOffset now)
        {
            var date = DateTimeOffset.UtcNow.StartOfDay();
            while (date.DayOfWeek != DayOfWeek.Saturday)
                date += TimeSpan.FromDays(1);
            var time = date + TimeSpan.FromHours(2);
            return now > time ? time + TimeSpan.FromDays(7) : time;
        }
    }
}