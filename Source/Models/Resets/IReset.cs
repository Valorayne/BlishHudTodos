using System;

namespace Todos.Source.Models.Resets
{
    public interface IReset
    {
        TodoScheduleType Type { get; }
        
        string DropdownEntry { get; }
        string DropdownEntryTooltip { get; }
        
        bool IsDone(DateTime lastExecution);
        string IconTooltip(DateTime? lastExecution);
    }
}