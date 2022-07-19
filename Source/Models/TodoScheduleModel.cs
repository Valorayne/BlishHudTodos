using System;
using System.Collections.Generic;
using System.Linq;
using Todos.Source.Models.Resets;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoScheduleModel : ModelBase
    {
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
            ScheduleDropdown = Add(Variables.Persistent(ResetFactory.FromType(json.Type).DropdownEntry, v => json.Type = ResetFactory.FromDropdown(v).Type, json.Persist));
            LocalTime = Add(Variables.Persistent(json.LocalTime, v => json.LocalTime = v, json.Persist));
            Duration = Add(Variables.Persistent(json.Duration, v => json.Duration = v, json.Persist));
            
            Executions = Add(Variables.Persistent(json.Executions, v => json.Executions = v, json.Persist));

            Reset = Add(ScheduleDropdown.Select(ResetFactory.FromDropdown));
            LastExecution = Add(Executions.Select(executions => executions.Count > 0 ? executions.Max().WithoutSeconds() : (DateTime?) null));
            
            IsDone = Add(TimeService.NewMinute.CombineWith(LastExecution, Reset, LocalTime, Duration,
                (now, lastExecution, schedule, localTime, duration) => lastExecution.HasValue 
                                                                       && schedule.IsDone(now, lastExecution.Value, localTime, duration)));

            IconTooltip = Add(TimeService.NewMinute.CombineWith(LastExecution, Reset, LocalTime, Duration,
                (now, lastExecution, schedule, localTime, duration) => schedule.IconTooltip(now, lastExecution, localTime, duration)));
        }

        public void ToggleDone()
        {
            if (!IsDone.Value) Executions.Value = Executions.Value.Append(DateTime.Now.WithoutSeconds()).ToList();
            else if (Executions.Value.Count > 0)
            {
                var latestExecution = Executions.Value.Max();
                Executions.Value = Executions.Value.Where(execution => execution != latestExecution).ToList();
            }
        }
    }
}