using System;
using Microsoft.IdentityModel.Tokens;

namespace Todos.Source.Utils
{
    public static class DateUtils
    {
        public static DateTimeOffset WithoutSeconds(this DateTimeOffset time)
        {
            return new DateTimeOffset(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, time.Offset);
        }
        
        public static DateTimeOffset StartOfDay(this DateTimeOffset time)
        {
            return new DateTimeOffset(time.Year, time.Month, time.Day, 0, 0, 0, time.Offset);
        }
        
        public static string ToDaysSinceString(this DateTimeOffset since)
        {
            var daysSince = (DateTimeOffset.Now.Date - since.Date).Days;
            return daysSince == 0
                ? "today" 
                : daysSince == 1
                    ? "yesterday" 
                    : $"{daysSince} days ago";
        }
        
        public static string ToDurationString(this DateTimeOffset target)
        {
            var until = target - DateTimeOffset.Now.WithoutSeconds();
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
    }
}