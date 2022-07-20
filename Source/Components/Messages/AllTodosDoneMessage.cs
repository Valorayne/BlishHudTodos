using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Models;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Messages
{
    public sealed class AllTodosDoneMessage : CenteredMessage
    {
        private const string TEXT =
            "Everything done for today!\r\nTime to get some rest...\r\n...or to simply enjoy the game! :D";

        private static readonly Point LOCATION = new Point(0, 25);

        private readonly IProperty<bool> _subscription;

        public AllTodosDoneMessage(TodoListModel todoList) : base(TEXT, LOCATION)
        {
            _subscription = todoList.AllTodos.CombineWith(todoList.VisibleTodos, 
                (all, visible) => all.Count > 0 && visible.Count == 0)
                .Subscribe(this, v => Visible = v);
        }

        protected override void DisposeControl()
        {
            _subscription.Dispose();
            base.DisposeControl();
        }
    }
}