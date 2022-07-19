using System;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class WeeklyReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.WeeklyServer;
        
        public string DropdownEntry => "Weekly Server Reset";
        public string DropdownEntryTooltip => "This task will reset every Monday, 7:30 UTC";
        
        private static DateTime LastWeeklyReset(DateTime now) => NextWeeklyReset(now) - TimeSpan.FromDays(7);
        private static DateTime NextWeeklyReset(DateTime now)
        {
            var date = DateTime.UtcNow.Date;
            while (date.DayOfWeek != DayOfWeek.Monday)
                date += TimeSpan.FromDays(1);
            var time = date + TimeSpan.FromHours(7) + TimeSpan.FromMinutes(30);
            return now > time ? time + TimeSpan.FromDays(7) : time;
        }

        public bool IsDone(DateTime now, DateTime lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return lastExecution > LastWeeklyReset(now);
        }

        public string IconTooltip(DateTime now, DateTime? lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return $"Weekly reset in {NextWeeklyReset(now).ToDurationString()}";
        }
    }
}