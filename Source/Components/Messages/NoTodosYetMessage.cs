using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Models;

namespace Todos.Source.Components.Messages
{
    public sealed class NoTodosYetMessage : CenteredMessage
    {
        private const string TEXT =
            "Nothing to do yet!\r\nUse the plus button above to\r\nstart working towards your goals!";

        private static readonly Point LOCATION = new Point(0, 25);
        private readonly SettingsModel _settings;

        private readonly TodoListModel _todoList;

        public NoTodosYetMessage(TodoListModel todoList, SettingsModel settings) : base(TEXT, LOCATION)
        {
            _todoList = todoList;
            _settings = settings;
            _todoList.AllTodos.Subscribe(this, newValue => Visible = newValue.Count == 0);
        }

        protected override CaptureType CapturesInput()
        {
            return _settings.ClickThroughBackground.Value ? CaptureType.MouseWheel : base.CapturesInput();
        }

        protected override void DisposeControl()
        {
            _todoList.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}