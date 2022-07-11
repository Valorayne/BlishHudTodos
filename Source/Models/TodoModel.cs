using System;
using System.Linq;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoModel
    {
        public delegate void DeletionEvent(TodoModel todo);
        public delegate void ValueChangedEvent<in T>(T newValue);
        
        private readonly TodoJson _json;
        
        public int Version => _json.Version;
        public DateTime CreatedAt => _json.CreatedAt;

        public event ValueChangedEvent<string> DescriptionChanged;
        public string Description
        {
            get => _json.Description;
            set
            {
                if (value == _json.Description)
                    return;
 
                _json.Description = value;
                _json.Persist();
                DescriptionChanged?.Invoke(value);
            }
        }

        public event ValueChangedEvent<TodoSchedule?> ScheduleChanged;
        public TodoSchedule? Schedule
        {
            get => _json.Schedule;
            set
            {
                if (Equals(value, _json.Schedule))
                    return;

                _json.Schedule = value;
                _json.Persist();
                ScheduleChanged?.Invoke(value);
            }
        }

        public event ValueChangedEvent<string> ClipboardContentChanged;
        public string ClipboardContent
        {
            get => _json.ClipboardContent;
            set
            {
                if (ClipboardContent == _json.ClipboardContent)
                    return;

                _json.ClipboardContent = value;
                _json.Persist();
                ClipboardContentChanged?.Invoke(value);
            }
        }
        
        public bool IsNew { get; set; }

        public TodoModel(TodoJson json, bool isNew)
        {
            _json = json;
            IsNew = isNew;
            if (isNew)
                _json.Persist();
        }

        public event ValueChangedEvent<DateTime?> LastExecutionChanged;
        public DateTime? LastExecution => _json.Executions.Count > 0 
            ? _json.Executions.Max().WithoutSeconds() 
            : (DateTime?)null;

        public event ValueChangedEvent<bool> DoneChanged; 
        public bool Done
        {
            get
            {
                var lastExecution = LastExecution;
                if (!Schedule.HasValue)
                    return lastExecution.HasValue;

                switch (Schedule.Value.Type)
                {
                    case TodoScheduleType.DailyServer:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastDailyReset;
                    case TodoScheduleType.WeeklyServer:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastWeeklyReset;
                    case TodoScheduleType.LocalTime:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastLocalReset(Schedule.Value);
                    case TodoScheduleType.Duration:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastDurationReset(Schedule.Value);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (value) 
                    _json.Executions.Add(DateTime.Now.WithoutSeconds());
                else
                {
                    if (_json.Executions.Count > 0)
                        _json.Executions.RemoveAt(_json.Executions.IndexOf(_json.Executions.Max()));
                }
                _json.Persist();
                DoneChanged?.Invoke(value);
                LastExecutionChanged?.Invoke(LastExecution);
            }
        }

        public event DeletionEvent Deleted;
        public void Delete()
        {
            _json.IsDeleted = true;
            _json.Persist();
            Deleted?.Invoke(this);
        }
    }
}