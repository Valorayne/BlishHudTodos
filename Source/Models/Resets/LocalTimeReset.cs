using System;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class LocalTimeReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.LocalTime;
        
        public string DropdownEntry => TodoScheduleModel.LOCAL_TIME;
        public string DropdownEntryTooltip => "This task will reset every day at the local time specified below";

        private readonly Variable<TimeSpan> _localTime;

        public LocalTimeReset(Variable<TimeSpan> localTime)
        {
            _localTime = localTime;
        }

        public bool IsDone(DateTime lastExecution) => lastExecution > LastLocalReset;
        public string IconTooltip(DateTime? lastExecution) => $"Local reset in {NextLocalReset.ToDurationString()}";

        private DateTime LastLocalReset
        {
            get
            {
                var resetToday = DateTime.Today + _localTime.Value;
                return DateTime.Now > resetToday ? resetToday : resetToday - TimeSpan.FromDays(1);
            }
        }
        
        private DateTime NextLocalReset
        {
            get
            {
                var resetToday = DateTime.Today + _localTime.Value;
                return DateTime.Now < resetToday ? resetToday : resetToday + TimeSpan.FromDays(1);
            }
        }
    }
}