using System.Linq;
using Blish_HUD;
using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class AllTodosDoneMessage : CenteredMessage
    {
        private const string TEXT =
            "Everything done for today!\r\nTime to get some rest...\r\n...or to simply enjoy the game! :D";

        private static readonly Point LOCATION = new Point(0, 25);

        public AllTodosDoneMessage() : base(TEXT, LOCATION)
        {
            UpdateVisibility(this);

            Data.TodoAdded += UpdateVisibility;
            Data.TodoModified += UpdateVisibility;
            Data.TodoDeleted += UpdateVisibility;
            Settings.ShowAlreadyDoneTasks.SettingChanged += SettingChanged;
        }

        private void SettingChanged(object sender, ValueChangedEventArgs<bool> e)
        {
            UpdateVisibility(sender);
        }

        protected override void DisposeControl()
        {
            Data.TodoAdded -= UpdateVisibility;
            Data.TodoModified -= UpdateVisibility;
            Data.TodoDeleted -= UpdateVisibility;
            Settings.ShowAlreadyDoneTasks.SettingChanged -= SettingChanged;

            base.DisposeControl();
        }

        private void UpdateVisibility(object sender, Todo e = null)
        {
            Visible = !Settings.ShowAlreadyDoneTasks.Value && Data.Todos.Count > 0 && Data.Todos.All(todo => todo.Done);
        }
    }
}