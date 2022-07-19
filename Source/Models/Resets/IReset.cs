using System;

namespace Todos.Source.Models.Resets
{
    public interface IReset
    {
        TodoScheduleType Type { get; }
        
        string DropdownEntry { get; }
        string DropdownEntryTooltip { get; }
        
        bool IsDone(DateTime now, DateTime lastExecution, TimeSpan localTime, TimeSpan duration);
        string IconTooltip(DateTime now, DateTime? lastExecution, TimeSpan localTime, TimeSpan duration);
    }
}