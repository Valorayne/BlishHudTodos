using System;
using System.Collections.Generic;
using System.Linq;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models.Resets
{
    public class PsnaReset : IReset
    {
        private static readonly IDictionary<string, IDictionary<DayOfWeek, string>> VENDORS =
            new Dictionary<string, IDictionary<DayOfWeek, string>>
            {
                {
                    "Mehem", new Dictionary<DayOfWeek, string>
                    {
                        { DayOfWeek.Monday, "[&BIcHAAA=]" },
                        { DayOfWeek.Tuesday, "[&BH8HAAA=]" },
                        { DayOfWeek.Wednesday, "[&BH4HAAA=]" },
                        { DayOfWeek.Thursday, "[&BKsHAAA=]" },
                        { DayOfWeek.Friday, "[&BJQHAAA=]" },
                        { DayOfWeek.Saturday, "[&BH8HAAA=]" },
                        { DayOfWeek.Sunday, "[&BIkHAAA=]" }
                    }
                },
                {
                    "Fox", new Dictionary<DayOfWeek, string>
                    {
                        { DayOfWeek.Monday, "[&BEwDAAA=]" },
                        { DayOfWeek.Tuesday, "[&BEgAAAA=]" },
                        { DayOfWeek.Wednesday, "[&BMIBAAA=]" },
                        { DayOfWeek.Thursday, "[&BE8AAAA=]" },
                        { DayOfWeek.Friday, "[&BMMCAAA=]" },
                        { DayOfWeek.Saturday, "[&BLkCAAA=]" },
                        { DayOfWeek.Sunday, "[&BDoBAAA=]" }
                    }
                },
                {
                    "Derwena", new Dictionary<DayOfWeek, string>
                    {
                        { DayOfWeek.Monday, "[&BKYBAAA=]" },
                        { DayOfWeek.Tuesday, "[&BBkAAAA=]" },
                        { DayOfWeek.Wednesday, "[&BKYAAAA=]" },
                        { DayOfWeek.Thursday, "[&BIMAAAA=]" },
                        { DayOfWeek.Friday, "[&BNUGAAA=]" },
                        { DayOfWeek.Saturday, "[&BJIBAAA=]" },
                        { DayOfWeek.Sunday, "[&BC0AAAA=]" }
                    }
                },
                {
                    "Yana", new Dictionary<DayOfWeek, string>
                    {
                        { DayOfWeek.Monday, "[&BNIEAAA=]" },
                        { DayOfWeek.Tuesday, "[&BKgCAAA=]" },
                        { DayOfWeek.Wednesday, "[&BP0CAAA=]" },
                        { DayOfWeek.Thursday, "[&BP0DAAA=]" },
                        { DayOfWeek.Friday, "[&BJsCAAA=]" },
                        { DayOfWeek.Saturday, "[&BBEDAAA=]" },
                        { DayOfWeek.Sunday, "[&BO4CAAA=]" }
                    }
                },
                {
                    "Katelyn", new Dictionary<DayOfWeek, string>
                    {
                        { DayOfWeek.Monday, "[&BIMCAAA=]" },
                        { DayOfWeek.Tuesday, "[&BGQCAAA=]" },
                        { DayOfWeek.Wednesday, "[&BDgDAAA=]" },
                        { DayOfWeek.Thursday, "[&BF0GAAA=]" },
                        { DayOfWeek.Friday, "[&BHsBAAA=]" },
                        { DayOfWeek.Saturday, "[&BEICAAA=]" },
                        { DayOfWeek.Sunday, "[&BIUCAAA=]" }
                    }
                },
                {
                    "Verma", new Dictionary<DayOfWeek, string>
                    {
                        { DayOfWeek.Monday, "[&BA8CAAA=]" },
                        { DayOfWeek.Tuesday, "[&BIMBAAA=]" },
                        { DayOfWeek.Wednesday, "[&BPEBAAA=]" },
                        { DayOfWeek.Thursday, "[&BOcBAAA=]" },
                        { DayOfWeek.Friday, "[&BNMAAAA=]" },
                        { DayOfWeek.Saturday, "[&BBABAAA=]" },
                        { DayOfWeek.Sunday, "[&BCECAAA=]" }
                    }
                }
            };

        private static DateTimeOffset LastDailyReset => DateTimeOffset.UtcNow.StartOfDay();
        private static DateTimeOffset NextDailyReset => DateTimeOffset.UtcNow.StartOfDay() + TimeSpan.FromDays(1);
        private static DateTimeOffset DailyLocationReset => LastDailyReset + TimeSpan.FromHours(8);

        public TodoScheduleType Type => TodoScheduleType.Psna;

        public string DropdownEntry => "Pact Supply Network Agent";
        public string DropdownEntryTooltip => "PSNA Offerings will reset every day at 0:00 UTC";

        public bool IsDone(DateTimeOffset now, DateTimeOffset lastExecution, TimeSpan localTime, TimeSpan duration)
        {
            return lastExecution > LastDailyReset;
        }

        public string IconTooltip(DateTimeOffset now, DateTimeOffset? lastExecution, TimeSpan localTime,
            TimeSpan duration)
        {
            return $"PSNA Offerings reset in {NextDailyReset.ToDurationString()}";
        }

        public string ClipboardContent(DateTimeOffset now)
        {
            var isNewDay = now >= DailyLocationReset;
            var dayOfWeek = isNewDay ? now.DayOfWeek : (now - TimeSpan.FromDays(1)).DayOfWeek;

            return VENDORS.Keys.Select(vendor => $"{vendor}@{VENDORS[vendor][dayOfWeek]}")
                .Aggregate((string)null, (result, s) => result != null ? $"{result} {s}" : s);
        }
    }
}