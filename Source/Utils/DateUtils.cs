using System;

namespace TodoList
{
    public static class DateUtils
    {
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
            var dayString = until.Days == 0 ? "" : until.Days == 1 ? "1 day, " : $"{until.Days} days, ";
            var hourString = until.Hours == 0 ? "" : $"{until.Hours}h ";
            var minuteString = until.Minutes == 0 
                ? dayString.Length > 0 || hourString.Length > 0 ? "" : "less than a minute" 
                : $"{until.Minutes}min";
            return $"{dayString}{hourString}{minuteString}";
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
                return date + TimeSpan.FromHours(7) + TimeSpan.FromMinutes(30);
            }
        }
    }
}