using System;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoModel : IDisposable
    {
        public readonly IVariable<bool> IsDeleted;
        public readonly IVariable<bool> IsEditing;
        public readonly IProperty<bool> IsVisible;

        public readonly IVariable<long> OrderIndex;
        public readonly IVariable<string> Description;
        public readonly IVariable<string> ClipboardContent;

        public readonly TodoScheduleModel Schedule;

        public TodoModel(TodoJson json, bool isNew)
        {
            Schedule = new TodoScheduleModel(json.Schedule);
            
            OrderIndex = Variables.Persistent(json.OrderIndex, v => json.OrderIndex = v, json.Persist);
            Description = Variables.Persistent(json.Description, v => json.Description = v, json.Persist);
            ClipboardContent = Variables.Persistent(json.ClipboardContent, v => json.ClipboardContent = v, json.Persist);
            
            IsDeleted = Variables.Persistent(false, v => json.IsDeleted = v, json.Persist);
            IsEditing = Variables.Transient(isNew);

            IsVisible = Schedule.IsDone.CombineWith(IsEditing, Settings.ShowAlreadyDoneTasks,
                (done, editing, show) => !done || editing || show);
        }

        public void Dispose()
        {
            Schedule.Dispose();
            
            OrderIndex.Dispose();
            Description.Dispose();
            ClipboardContent.Dispose();
            
            IsDeleted.Dispose();
            IsEditing.Dispose();
            IsVisible.Dispose();
        }
    }
}