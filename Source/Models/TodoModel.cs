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

        public readonly IVariable<bool> IsDeleted;
        public readonly IProperty<bool> IsVisible;
        public readonly IVariable<bool> IsEditing;
        
        public readonly IVariable<long> OrderIndex;
        public readonly IVariable<string> Description;
        public readonly IVariable<string> ClipboardContent;

        public readonly TodoScheduleModel Schedule;
        public readonly IVariable<bool> IsDone;
        
        public string IconTooltip => Schedule.Reset.Value.IconTooltip(LastExecution);
        public DateTime? LastExecution => _json.Schedule.Executions.Count > 0 ? _json.Schedule.Executions.Max().WithoutSeconds() : (DateTime?)null;

        public TodoModel(TodoJson json, bool isNew)
        {
            _json = json;
            
            Schedule = new TodoScheduleModel(_json.Schedule, _json.Persist);
            
            OrderIndex = Variables.Persistent(_json.OrderIndex, v => _json.OrderIndex = v, _json.Persist);
            Description = Variables.Persistent(_json.Description, v => _json.Description = v, _json.Persist);
            ClipboardContent = Variables.Persistent(_json.ClipboardContent, v => _json.ClipboardContent = v, _json.Persist);
            
            IsDeleted = Variables.Persistent(false, v => _json.IsDeleted = v, _json.Persist);
            IsEditing = Variables.Transient(isNew);
            IsDone = Variables.Persistent(Done, v => Done = v, _json.Persist);

            IsVisible = IsDone.CombineWith(IsEditing, Settings.ShowAlreadyDoneTasks,
                (done, editing, show) => !done || editing || show);
            
            TimeService.NewMinute += OnNewMinute;
        }

        private void OnNewMinute(object sender, GameTime e) => ((Variable<bool>)IsDone).Value = Done;

        private bool Done
        {
            get => LastExecution.HasValue && Schedule.Reset.Value.IsDone(LastExecution.Value);
            set
            {
                var executions = _json.Schedule.Executions;
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