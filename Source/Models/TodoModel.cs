using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoModel : IDisposable
    {
        private readonly TodoJson _json;

        public readonly Variable<bool> IsDeleted;
        public readonly Variable<bool> IsVisible;
        public readonly Variable<bool> IsEditing;
        
        public readonly Variable<long> OrderIndex;
        public readonly Variable<string> Description;
        public readonly Variable<string> ClipboardContent;

        public readonly TodoScheduleModel Schedule;
        public readonly Variable<bool> IsDone;
        
        public string IconTooltip => Schedule.Reset.Value.IconTooltip(LastExecution);
        public DateTime? LastExecution => _json.Executions.Count > 0 ? _json.Executions.Max().WithoutSeconds() : (DateTime?)null;

        public TodoModel(TodoJson json, bool isNew)
        {
            _json = json;
            
            Schedule = new TodoScheduleModel(_json.Schedule, _json.Persist);
            
            OrderIndex = new Variable<long>(this, _json.OrderIndex, v => _json.OrderIndex = v, _json.Persist);
            Description = new Variable<string>(this, _json.Description, v => _json.Description = v, _json.Persist);
            ClipboardContent = new Variable<string>(this, _json.ClipboardContent, v => _json.ClipboardContent = v, _json.Persist);
            
            IsDeleted = new Variable<bool>(this, false, v => _json.IsDeleted = v, _json.Persist);
            IsEditing = new Variable<bool>(this, isNew);
            IsDone = new Variable<bool>(this, Done, v => Done = v, _json.Persist);

            IsVisible = Variable<bool>.Combine(IsDone, IsEditing, Settings.ShowAlreadyDoneTasks,
                (done, editing, show) => !done || editing || show);
            TimeService.NewMinute += OnNewMinute;
        }

        private void OnNewMinute(object sender, GameTime e) => IsDone.Value = Done;

        private bool Done
        {
            get => LastExecution.HasValue && Schedule.Reset.Value.IsDone(LastExecution.Value);
            set
            {
                var executions = _json.Executions;
                if (value) executions.Add(DateTime.Now.WithoutSeconds());
                else if (executions.Count > 0) executions.RemoveAt(executions.IndexOf(executions.Max()));
            }
        }

        public void Dispose()
        {
            Schedule.Dispose();
            
            OrderIndex.Dispose();
            Description.Dispose();
            ClipboardContent.Dispose();
            
            IsDeleted.Dispose();
            IsEditing.Dispose();
            IsDone.Dispose();
            
            IsVisible.Dispose();
            TimeService.NewMinute -= OnNewMinute;
        }
    }
}