using System.Collections.Generic;
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
            Data.AllTodos.Subscribe(this, UpdateVisibility);
            Data.VisibleTodos.Subscribe(this, UpdateVisibility);
        }

        private void UpdateVisibility(IReadOnlyList<TodoModel> newValue)
        {
            Visible = Data.AllTodos.Value.Count > 0 && Data.VisibleTodos.Value.Count == 0;
        }

        protected override void DisposeControl()
        {
            Data.AllTodos.Unsubscribe(this);
            Data.VisibleTodos.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}