using System;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class DurationReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.Duration;
        
        public string DropdownEntry => "Duration";
        public string DropdownEntryTooltip => "This task will reset after the duration specified below has passed";

        public bool IsDone(DateTime now, DateTime lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return lastExecution > now - duration;
        }

        public string IconTooltip(DateTime now, DateTime? lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return !lastExecution.HasValue || !IsDone(now, lastExecution.Value, localTime, duration)
                ? $"Duration reset after {(now + duration).ToDurationString()}"
                : $"Duration reset in {(lastExecution.Value + duration).ToDurationString()}";
        }
    }
}