using System;

namespace Todos.Source.Models.Resets
{
    public interface IReset
    {
        string DropdownEntry { get; }
        string DropdownEntryTooltip { get; }
        
        bool IsDone(DateTime lastExecution);
        string IconTooltip(DateTime? lastExecution);
    }
}