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
        
        private readonly TodoListModel _todoList;

        public NoTodosYetMessage(TodoListModel todoList) : base(TEXT, LOCATION)
        {
            _todoList = todoList;
            _todoList.AllTodos.Subscribe(this, newValue => Visible = newValue.Count == 0);
        }

        protected override void DisposeControl()
        {
            _todoList.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}