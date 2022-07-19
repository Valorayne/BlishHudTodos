using System.Collections.Generic;
using System.Linq;

namespace Todos.Source.Models.Resets
{
    public static class ResetFactory
    {
        private static readonly List<IReset> _allResets = new List<IReset>
        {
            new NoReset(),
            new DailyReset(),
            new WeeklyReset(),
            new LocalTimeReset(),
            new DurationReset()
        };

        public static IEnumerable<string> AllDropdownEntries => _allResets.Select(r => r.DropdownEntry);

        private static readonly Dictionary<TodoScheduleType, IReset> _resetsByType = _allResets.ToDictionary(r => r.Type);
        private static readonly Dictionary<string, IReset> _resetsByDropdownEntry = _allResets.ToDictionary(r => r.DropdownEntry);

        public static IReset FromType(TodoScheduleType type) => _resetsByType[type];
        public static IReset FromDropdown(string dropdownEntry) => _resetsByDropdownEntry[dropdownEntry];
    }
}