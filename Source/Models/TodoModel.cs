using System;
using System.Linq;
using Blish_HUD;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoModel : IDisposable
    {
        public delegate void DeletionEvent(TodoModel todo);
        public delegate void ValueChangedEvent<in T>(T newValue);
        
        private readonly TodoJson _json;
        
        public int Version => _json.Version;
        public DateTime CreatedAt => _json.CreatedAt;

        public readonly Variable<bool> IsVisible;
        public readonly Variable<bool> IsEditing;
        public readonly Variable<bool> CanBeMovedUp;
        public readonly Variable<bool> CanBeMovedDown;

        public readonly Variable<string> Description;
        public readonly Variable<TodoSchedule?> Schedule;
        public readonly Variable<string> ClipboardContent;

        public TodoModel(TodoJson json, bool isNew)
        {
            _json = json;
            if (isNew)
                _json.Persist();
            
            Description = new Variable<string>(_json.Description, v => _json.Description = v, _json.Persist);
            Schedule = new Variable<TodoSchedule?>(_json.Schedule, v => _json.Schedule = v, _json.Persist);
            ClipboardContent = new Variable<string>(_json.ClipboardContent, v => _json.ClipboardContent = v, _json.Persist);

            IsEditing = new Variable<bool>(isNew, _ => IsVisible.Value = ShouldBeVisible);
            IsVisible = new Variable<bool>(ShouldBeVisible);
            CanBeMovedUp = new Variable<bool>(false);
            CanBeMovedDown = new Variable<bool>(false);

            Settings.ShowAlreadyDoneTasks.SettingChanged += OnShowTasksSettingChanged;
        }

        private void OnShowTasksSettingChanged(object sender, ValueChangedEventArgs<bool> e)
        {
            IsVisible.Value = ShouldBeVisible;
        }

        private bool ShouldBeVisible => !Done || IsEditing.Value || Settings.ShowAlreadyDoneTasks.Value;

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
                if (!Schedule.Value.HasValue)
                    return lastExecution.HasValue;

                switch (Schedule.Value.Value.Type)
                {
                    case TodoScheduleType.DailyServer:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastDailyReset;
                    case TodoScheduleType.WeeklyServer:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastWeeklyReset;
                    case TodoScheduleType.LocalTime:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastLocalReset(Schedule.Value.Value);
                    case TodoScheduleType.Duration:
                        return lastExecution.HasValue && lastExecution.Value > DateUtils.LastDurationReset(Schedule.Value.Value);
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
                IsVisible.Value = ShouldBeVisible;
            }
        }

        public event DeletionEvent Deleted;
        public void Delete()
        {
            _json.IsDeleted = true;
            _json.Persist();
            Deleted?.Invoke(this);
        }

        public void Dispose()
        {
            Settings.ShowAlreadyDoneTasks.SettingChanged -= OnShowTasksSettingChanged;
        }
    }
}