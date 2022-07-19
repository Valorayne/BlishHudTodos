using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source.Models
{
    public class TodoModel : ModelBase
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
            
            OrderIndex = Add(Variables.Persistent(json.OrderIndex, v => json.OrderIndex = v, json.Persist));
            Description = Add(Variables.Persistent(json.Description, v => json.Description = v, json.Persist));
            ClipboardContent = Add(Variables.Persistent(json.ClipboardContent, v => json.ClipboardContent = v, json.Persist));
            
            IsDeleted = Add(Variables.Persistent(false, v => json.IsDeleted = v, json.Persist));
            IsEditing = Add(Variables.Transient(isNew));

            IsVisible = Add(Schedule.IsDone.CombineWith(IsEditing, Settings.ShowAlreadyDoneTasks,
                (done, editing, show) => !done || editing || show));
        }

        protected override void DisposeModel() => Schedule.Dispose();
    }
}