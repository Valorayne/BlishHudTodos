using System;
using System.Linq;
using Blish_HUD;
using Microsoft.Xna.Framework;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoModel : IDisposable
    {
        public delegate void ValueChangedEvent<in T>(T newValue);
        
        private readonly TodoJson _json;

        public readonly Variable<bool> IsDeleted;
        public readonly Variable<bool> IsVisible;
        public readonly Variable<bool> IsEditing;
        public readonly Variable<long> OrderIndex;

        public readonly Variable<string> Description;
        public readonly Variable<string> ClipboardContent;

        public readonly TodoScheduleModel Schedule;

        public TodoModel(TodoJson json, bool isNew)
        {
            _json = json;
            if (isNew)
                _json.Persist();

            IsDeleted = new Variable<bool>(this, false, v => _json.IsDeleted = v, _json.Persist);
            
            Description = new Variable<string>(this, _json.Description, v => _json.Description = v, _json.Persist);
            ClipboardContent = new Variable<string>(this, _json.ClipboardContent, v => _json.ClipboardContent = v, _json.Persist);

            Schedule = new TodoScheduleModel(_json.Schedule, _json.Persist);

            // ReSharper disable once PossibleNullReferenceException
            IsEditing = new Variable<bool>(this, isNew, _ => IsVisible.Value = ShouldBeVisible);
            IsVisible = new Variable<bool>(this, ShouldBeVisible);
            OrderIndex = new Variable<long>(this, _json.OrderIndex, v => _json.OrderIndex = v, _json.Persist);

            Settings.ShowAlreadyDoneTasks.SettingChanged += OnShowTasksSettingChanged;
            TimeService.NewMinute += OnNewMinute;
        }

        private void OnNewMinute(object sender, GameTime e)
        {
            IsVisible.Value = ShouldBeVisible;
        }

        private void OnShowTasksSettingChanged(object sender, ValueChangedEventArgs<bool> e)
        {
            IsVisible.Value = ShouldBeVisible;
        }

        private bool ShouldBeVisible => !Done || IsEditing.Value || Settings.ShowAlreadyDoneTasks.Value;
        public string IconTooltip => Schedule.Reset.Value.IconTooltip(LastExecution);

        public DateTime? LastExecution => _json.Executions.Count > 0 
            ? _json.Executions.Max().WithoutSeconds() 
            : (DateTime?)null;

        public event ValueChangedEvent<bool> DoneChanged; 
        public bool Done
        {
            get => LastExecution.HasValue && Schedule.Reset.Value.IsDone(LastExecution.Value);
            set
            {
                if (value == Done)
                    return;
                
                if (value) 
                    _json.Executions.Add(DateTime.Now.WithoutSeconds());
                else
                {
                    if (_json.Executions.Count > 0)
                        _json.Executions.RemoveAt(_json.Executions.IndexOf(_json.Executions.Max()));
                }
                _json.Persist();
                DoneChanged?.Invoke(value);
                IsVisible.Value = ShouldBeVisible;
            }
        }

        public void Dispose()
        {
            Settings.ShowAlreadyDoneTasks.SettingChanged -= OnShowTasksSettingChanged;
            TimeService.NewMinute -= OnNewMinute;
        }
    }
}