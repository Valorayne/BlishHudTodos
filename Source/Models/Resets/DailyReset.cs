using System;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class DailyReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.DailyServer;
        
        public string DropdownEntry => "Daily Server Reset";
        public string DropdownEntryTooltip => "This task will reset every day at 0:00 UTC";
        
        private static DateTime LastDailyReset => DateTime.UtcNow.Date;
        private static DateTime NextDailyReset => DateTime.UtcNow.Date + TimeSpan.FromDays(1);

        public bool IsDone(DateTime lastExecution, TimeSpan localTime, TimeSpan duration) => lastExecution > LastDailyReset;
        public string IconTooltip(DateTime? lastExecution, TimeSpan localTime, TimeSpan duration) => $"Daily reset in {NextDailyReset.ToDurationString()}";
    }
}