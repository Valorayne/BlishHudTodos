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
            UpdateVisibility(this);

            Data.TodoAdded += UpdateVisibility;
            Data.TodoDeleted += UpdateVisibility;
        }

        protected override void DisposeControl()
        {
            Data.TodoAdded -= UpdateVisibility;
            Data.TodoDeleted -= UpdateVisibility;
            base.DisposeControl();
        }

        private void UpdateVisibility(object sender, TodoModel e = null)
        {
            Visible = Data.Todos.Count == 0;
        }
    }
}