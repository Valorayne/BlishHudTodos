using System;
using Todos.Source.Persistence;

namespace Todos.Source.Models.Resets
{
    public class NoReset : IReset
    {
        public TodoScheduleType Type => TodoScheduleType.NoReset;

        public string DropdownEntry => "Never Resets";
        public string DropdownEntryTooltip => "This task will not reset automatically";

        public bool IsDone(DateTimeOffset now, DateTimeOffset lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return true;
        }

        public string IconTooltip(DateTimeOffset now, DateTimeOffset? lastExecution, TimeSpan localTime,
            TimeSpan duration)
        {
            return null;
        }

        public string ClipboardContent(DateTimeOffset now)
        {
            return null;
        }
    }
}