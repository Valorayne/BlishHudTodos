using System.Linq;
using Blish_HUD;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Messages
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
            Data.AnyDoneChanged += OnAnyDoneChanged;
            Data.TodoDeleted += UpdateVisibility;
            Settings.ShowAlreadyDoneTasks.SettingChanged += SettingChanged;
        }

        private void OnAnyDoneChanged(object sender, bool e)
        {
            UpdateVisibility(sender);
        }

        private void SettingChanged(object sender, ValueChangedEventArgs<bool> e)
        {
            UpdateVisibility(sender);
        }

        protected override void DisposeControl()
        {
            Data.TodoAdded -= UpdateVisibility;
            Data.AnyDoneChanged -= OnAnyDoneChanged;
            Data.TodoDeleted -= UpdateVisibility;
            Settings.ShowAlreadyDoneTasks.SettingChanged -= SettingChanged;

            base.DisposeControl();
        }

        private void UpdateVisibility(object sender, TodoModel e = null)
        {
            Visible = !Settings.ShowAlreadyDoneTasks.Value && Data.Todos.Count > 0 && Data.Todos.All(todo => todo.Done);
        }
    }
}