using System;
using Microsoft.IdentityModel.Tokens;

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
            var until = target - DateTime.Now.WithoutSeconds();
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