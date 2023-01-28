using System;
using Todos.Source.Persistence;

namespace Todos.Source.Models.Resets
{
    public interface IReset
    {
        TodoScheduleType Type { get; }

        string DropdownEntry { get; }
        string DropdownEntryTooltip { get; }

        bool IsDone(DateTimeOffset now, DateTimeOffset lastExecution, TimeSpan localTime, TimeSpan duration);
        string IconTooltip(DateTimeOffset now, DateTimeOffset? lastExecution, TimeSpan localTime, TimeSpan duration);

        string ClipboardContent(DateTimeOffset now);
    }
}