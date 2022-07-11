using System;
using Microsoft.IdentityModel.Tokens;
using Todos.Source.Models;

namespace Todos.Source.Utils
{
    public static class DateUtils
    {
        public static DateTime WithoutSeconds(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
        }
        
        public static string ToDaysSinceString(this DateTime since)
        {
            var daysSince = (DateTime.Today - since.Date).Days;
            return daysSince == 0
                ? "today" 
                : daysSince == 1
                    ? "yesterday" 
                    : $"{daysSince} days ago";
        }
        
        public static string ToDurationString(this DateTime target)
        {
            var until = target - DateTime.Now;
            var dayString = until.Days == 0 ? "" : until.Days == 1 ? "1 day" : $"{until.Days} days";
            var hourString = until.Hours == 0 ? "" : $"{until.Hours}h ";
            var minuteString = until.Minutes == 0 
                ? dayString.Length > 0 || hourString.Length > 0 ? "" : "less than a minute" 
                : $"{until.Minutes}min";
            var separator = !dayString.IsNullOrEmpty() && (!hourString.IsNullOrEmpty() || !minuteString.IsNullOrEmpty())
                ? ", "
                : "";
            return $"{dayString}{separator}{hourString}{minuteString}";
        }

        public static DateTime LastDailyReset => DateTime.UtcNow.Date;
        public static DateTime NextDailyReset => DateTime.UtcNow.Date + TimeSpan.FromDays(1);

        public static DateTime LastWeeklyReset => NextWeeklyReset - TimeSpan.FromDays(7);
        public static DateTime NextWeeklyReset
        {
            get
            {
                var date = DateTime.UtcNow.Date;
                while (date.DayOfWeek != DayOfWeek.Monday)
                    date += TimeSpan.FromDays(1);
                var time = date + TimeSpan.FromHours(7) + TimeSpan.FromMinutes(30);
                return DateTime.Now > time ? time + TimeSpan.FromDays(7) : time;
            }
        }

        public static DateTime NextLocalReset(TodoSchedule schedule)
        {
            var resetToday = DateTime.Today + schedule.LocalTime;
            return DateTime.Now < resetToday ? resetToday : resetToday + TimeSpan.FromDays(1);
        }

        public static DateTime LastLocalReset(TodoSchedule schedule)
        {
            var resetToday = DateTime.Today + schedule.LocalTime;
            return DateTime.Now > resetToday ? resetToday : resetToday - TimeSpan.FromDays(1);
        }
        
        public static DateTime NextDurationReset(TodoModel todo)
        {
            return (todo.LastExecution ?? DateTime.Now) + todo.Schedule.Value?.Duration ?? DateTime.Now;
        }

        public static DateTime LastDurationReset(TodoSchedule schedule)
        {
            return DateTime.Now - schedule.Duration; 
        }
    }
}