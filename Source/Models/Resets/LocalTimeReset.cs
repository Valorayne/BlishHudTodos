using System;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class LocalTimeReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.LocalTime;
        
        public string DropdownEntry => TodoScheduleModel.LOCAL_TIME;
        public string DropdownEntryTooltip => "This task will reset every day at the local time specified below";

        public bool IsDone(DateTime lastExecution, TimeSpan localTime, TimeSpan duration) => lastExecution > LastLocalReset(localTime);
        public string IconTooltip(DateTime? lastExecution, TimeSpan localTime, TimeSpan duration) => $"Local reset in {NextLocalReset(localTime).ToDurationString()}";

        private static DateTime LastLocalReset(TimeSpan localTime)
        {
            var resetToday = DateTime.Today + localTime;
            return DateTime.Now > resetToday ? resetToday : resetToday - TimeSpan.FromDays(1);
        }
        
        private static DateTime NextLocalReset(TimeSpan localTime)
        {
            var resetToday = DateTime.Today + localTime;
            return DateTime.Now < resetToday ? resetToday : resetToday + TimeSpan.FromDays(1);
        }
    }
}