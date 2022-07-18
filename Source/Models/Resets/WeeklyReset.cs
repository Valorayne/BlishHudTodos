using System;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class WeeklyReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.WeeklyServer;
        
        public string DropdownEntry => TodoScheduleModel.WEEKLY_SERVER;
        public string DropdownEntryTooltip => "This task will reset every Monday, 7:30 UTC";
        
        private static DateTime LastWeeklyReset => NextWeeklyReset - TimeSpan.FromDays(7);
        private static DateTime NextWeeklyReset
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

        public bool IsDone(DateTime lastExecution) => lastExecution > LastWeeklyReset;
        public string IconTooltip(DateTime? lastExecution) => $"Weekly reset in {NextWeeklyReset.ToDurationString()}";
    }
}