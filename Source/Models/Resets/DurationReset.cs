using System;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class DurationReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.Duration;
        
        public string DropdownEntry => TodoScheduleModel.DURATION;
        public string DropdownEntryTooltip => "This task will reset after the duration specified below has passed";

        private readonly Variable<TimeSpan> _duration;

        public DurationReset(Variable<TimeSpan> duration)
        {
            _duration = duration;
        }

        public bool IsDone(DateTime lastExecution) => lastExecution > LastDurationReset;
        public string IconTooltip(DateTime? lastExecution) => $"Duration reset in {NextDurationReset(lastExecution).ToDurationString()}";

        private DateTime NextDurationReset(DateTime? lastExecution)
        {
            var previousDurationReset = lastExecution ?? DateTime.Now;
            var nextDurationReset = previousDurationReset + _duration.Value;
            return nextDurationReset > DateTime.Now ? nextDurationReset : DateTime.Now + _duration.Value;
        }

        private DateTime LastDurationReset => DateTime.Now - _duration.Value; 
    }
}