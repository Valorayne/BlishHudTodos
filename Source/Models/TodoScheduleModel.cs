using System;
using Todos.Source.Models.Resets;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoScheduleModel
    {
        public const string NO_RESET = "Never Resets";
        public const string DAILY_SERVER = "Daily Server Reset";
        public const string WEEKLY_SERVER = "Weekly Server Reset";
        public const string LOCAL_TIME = "Local Time";
        public const string DURATION = "Duration";
        
        public readonly Variable<IReset> Reset;
        public readonly Variable<TimeSpan> LocalTime;
        public readonly Variable<TimeSpan> Duration;

        public TodoScheduleModel(TodoScheduleJson json, Action persist)
        {
            LocalTime = new Variable<TimeSpan>(json.LocalTime, v => json.LocalTime = v, persist);
            Duration = new Variable<TimeSpan>(json.Duration, v => json.Duration = v, persist);
            Reset = new Variable<IReset>(FromType(json.Type), null, persist);
        }

        public void UpdateSchedule(string dropdownEntry)
        {
            var newReset = FromString(dropdownEntry);
            if (newReset.GetType() != Reset.Value.GetType())
                Reset.Value = newReset;
        }

        private IReset FromString(string dropdownEntry)
        {
            switch (dropdownEntry)
            {
                case NO_RESET: return new NoReset();
                case DAILY_SERVER: return new DailyReset();
                case WEEKLY_SERVER: return new WeeklyReset();
                case LOCAL_TIME: return new LocalTimeReset(LocalTime);
                case DURATION: return new DurationReset(Duration);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private IReset FromType(TodoScheduleType type)
        {
            switch (type)
            {
                case TodoScheduleType.NoReset: return new NoReset();
                case TodoScheduleType.DailyServer: return new DailyReset();
                case TodoScheduleType.WeeklyServer: return new WeeklyReset();
                case TodoScheduleType.LocalTime: return new LocalTimeReset(LocalTime);
                case TodoScheduleType.Duration: return new DurationReset(Duration);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}