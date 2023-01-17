using System.Collections.Generic;
using System.Linq;
using Todos.Source.Persistence;

namespace Todos.Source.Models.Resets
{
    public static class ResetFactory
    {
        private static readonly List<IReset> _allResets = new List<IReset>
        {
            new NoReset(),
            new DailyReset(),
            new WeeklyReset(),
            new MapBonusRewardsReset(),
            new EuWvWReset(),
            new NaWvWReset(),
            new LocalTimeReset(),
            new DurationReset()
        };

        private static readonly Dictionary<TodoScheduleType, IReset> _resetsByType =
            _allResets.ToDictionary(r => r.Type);

        private static readonly Dictionary<string, IReset> _resetsByDropdownEntry =
            _allResets.ToDictionary(r => r.DropdownEntry);

        public static IEnumerable<string> AllDropdownEntries => _allResets.Select(r => r.DropdownEntry);

        public static IReset FromType(TodoScheduleType type)
        {
            return _resetsByType[type];
        }

        public static IReset FromDropdown(string dropdownEntry)
        {
            return _resetsByDropdownEntry[dropdownEntry];
        }
    }
}