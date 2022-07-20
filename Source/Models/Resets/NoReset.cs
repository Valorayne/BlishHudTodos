using System;
using Todos.Source.Persistence;

namespace Todos.Source.Models.Resets
{
    public class NoReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.NoReset;
        
        public string DropdownEntry => "Never Resets";
        public string DropdownEntryTooltip => "This task will not reset automatically";
        
        public bool IsDone(DateTime now, DateTime lastExecution, TimeSpan localTime, TimeSpan duration) => true;
        public string IconTooltip(DateTime now, DateTime? lastExecution, TimeSpan localTime, TimeSpan duration) => null;
    }
}