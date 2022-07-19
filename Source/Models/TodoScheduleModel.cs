using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Todos.Source.Models.Resets;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoScheduleModel : IDisposable
    {
        public const string NO_RESET = "Never Resets";
        public const string DAILY_SERVER = "Daily Server Reset";
        public const string WEEKLY_SERVER = "Weekly Server Reset";
        public const string LOCAL_TIME = "Local Time";
        public const string DURATION = "Duration";

        public readonly IVariable<string> ScheduleDropdown;
        public readonly IVariable<TimeSpan> LocalTime;
        public readonly IVariable<TimeSpan> Duration;
        
        private readonly IVariable<List<DateTime>> Executions;

        public readonly IProperty<IReset> Reset;
        public readonly IProperty<DateTime?> LastExecution;
        public readonly IProperty<bool> IsDone;
        public readonly IProperty<string> IconTooltip;

        public TodoScheduleModel(TodoScheduleJson json)
        {
            ScheduleDropdown = Variables.Persistent(FromType(json.Type).DropdownEntry, v => json.Type = FromString(v).Type, json.Persist);
            LocalTime = Variables.Persistent(json.LocalTime, v => json.LocalTime = v, json.Persist);
            Duration = Variables.Persistent(json.Duration, v => json.Duration = v, json.Persist);
            
            Executions = Variables.Persistent(json.Executions, v => json.Executions = v, json.Persist);

            Reset = ScheduleDropdown.Select(FromString);
            LastExecution = Executions.Select(executions => executions.Count > 0 ? executions.Max().WithoutSeconds() : (DateTime?) null);
            IsDone = LastExecution.CombineWith(Reset, (lastExecution, schedule) => lastExecution.HasValue && schedule.IsDone(lastExecution.Value));

            IconTooltip = LastExecution.CombineWith(Reset, (lastExecution, schedule) => schedule.IconTooltip(lastExecution));
            
            TimeService.NewMinute += OnNewMinute;
        }
        
        private void OnNewMinute(object sender, GameTime e) => ((Variable<bool>)IsDone).Value = LastExecution.Value.HasValue && Reset.Value.IsDone(LastExecution.Value.Value);

        public void ToggleDone()
        {
            if (!IsDone.Value) Executions.Value = Executions.Value.Append(DateTime.Now.WithoutSeconds()).ToList();
            else if (Executions.Value.Count > 0)
            {
                var latestExecution = Executions.Value.Max();
                Executions.Value = Executions.Value.Where(execution => execution != latestExecution).ToList();
            }
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

        public void Dispose()
        {
            ScheduleDropdown.Dispose();
            LocalTime.Dispose();
            Duration.Dispose();
            Executions.Dispose();
            
            Reset.Dispose();
            LastExecution.Dispose();
            IsDone.Dispose();
            
            IconTooltip.Dispose();
            
            TimeService.NewMinute -= OnNewMinute;
        }
    }
}