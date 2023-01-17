using System;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class MapBonusRewardsReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.MapBonusRewards;

        public string DropdownEntry => "Map Bonus Rewards Reset";
        public string DropdownEntryTooltip => "This task will reset every Thursday, 20:00 UTC";

        public bool IsDone(DateTimeOffset now, DateTimeOffset lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return lastExecution > LastWeeklyReset(now);
        }

        public string IconTooltip(DateTimeOffset now, DateTimeOffset? lastExecution, TimeSpan localTime,
            TimeSpan duration)
        {
            return $"Map Bonus Rewards reset in {NextWeeklyReset(now).ToDurationString()}";
        }

        private static DateTimeOffset LastWeeklyReset(DateTimeOffset now)
        {
            return NextWeeklyReset(now) - TimeSpan.FromDays(7);
        }

        private static DateTimeOffset NextWeeklyReset(DateTimeOffset now)
        {
            var date = DateTimeOffset.UtcNow.StartOfDay();
            while (date.DayOfWeek != DayOfWeek.Thursday)
                date += TimeSpan.FromDays(1);
            var time = date + TimeSpan.FromHours(20);
            return now > time ? time + TimeSpan.FromDays(7) : time;
        }
    }
}