using System;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class DurationReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.Duration;
        
        public string DropdownEntry => TodoScheduleModel.DURATION;
        public string DropdownEntryTooltip => "This task will reset after the duration specified below has passed";

        public bool IsDone(DateTime lastExecution, TimeSpan localTime, TimeSpan duration) => lastExecution > LastDurationReset(duration);
        public string IconTooltip(DateTime? lastExecution, TimeSpan localTime, TimeSpan duration) => $"Duration reset in {NextDurationReset(lastExecution, duration).ToDurationString()}";

        private static DateTime NextDurationReset(DateTime? lastExecution, TimeSpan duration)
        {
            var previousDurationReset = lastExecution ?? DateTime.Now;
            var nextDurationReset = previousDurationReset + duration;
            return nextDurationReset > DateTime.Now ? nextDurationReset : DateTime.Now + duration;
        }

        private static DateTime LastDurationReset(TimeSpan duration) => DateTime.Now - duration; 
    }
}