using System;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class LocalTimeReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.LocalTime;
        
        public string DropdownEntry => "Local Time";
        public string DropdownEntryTooltip => "This task will reset every day at the local time specified below";

        public bool IsDone(DateTime now, DateTime lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            var resetToday = DateTime.Today + localTime;
            var lastLocalReset = now > resetToday ? resetToday : resetToday - TimeSpan.FromDays(1);
            return lastExecution > lastLocalReset;
        }

        public string IconTooltip(DateTime now, DateTime? lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            var resetToday = DateTime.Today + localTime;
            var nextLocalReset = now < resetToday ? resetToday : resetToday + TimeSpan.FromDays(1);
            return $"Local reset in {nextLocalReset.ToDurationString()}";
        }
    }
}