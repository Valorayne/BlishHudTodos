using Microsoft.Xna.Framework;
using TodoList.Models;

namespace TodoList.Components
{
    public sealed class NoTodosYetMessage : CenteredMessage
    {
        private const string TEXT =
            "Nothing to do yet!\r\nUse the button on the top right\r\nto start working towards your goals!";

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

        private void UpdateVisibility(object sender, Todo e = null)
        {
            Visible = Data.Todos.Count == 0;
        }
    }
}