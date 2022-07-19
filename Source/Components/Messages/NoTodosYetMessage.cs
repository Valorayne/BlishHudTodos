using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Messages
{
    public sealed class NoTodosYetMessage : CenteredMessage
    {
        private const string TEXT =
            "Nothing to do yet!\r\nUse the plus button above to\r\nstart working towards your goals!";

        private static readonly Point LOCATION = new Point(0, 25);

        public NoTodosYetMessage() : base(TEXT, LOCATION)
        {
            Data.AllTodos.Subscribe(this, UpdateVisibility);
        }

        protected override void DisposeControl()
        {
            Data.AllTodos.Unsubscribe(this);
            base.DisposeControl();
        }

        private void UpdateVisibility(IReadOnlyList<TodoModel> newValue)
        {
            Visible = newValue.Count == 0;
        }
    }
}