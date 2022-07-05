using System;

namespace TodoList
{
    public static class DateUtils
    {
        public static string GetPastDayString(int days)
        {
            switch (days)
            {
                case 0: return "today";
                case 1: return "yesterday";
                default: return $"{days} days ago";
            }
        }
        
        public static string GetDurationString(DateTime target)
        {
            var until = target - DateTime.Now;
            var dayString = until.Days == 0 ? "" : until.Days == 1 ? "1 day, " : $"{until.Days} days, ";
            var hourString = until.Hours == 0 ? "" : $"{until.Hours}h ";
            var minuteString = until.Minutes == 0 ? "less than a minute" : $"{until.Minutes}min";
            return $"{dayString}{hourString}{minuteString}";
        }

        public static DateTime NextDailyReset => DateTime.UtcNow.Date + TimeSpan.FromDays(1);

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