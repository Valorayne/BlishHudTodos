using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public class TodoDetailsSaveButton : StandardButton
    {
        private readonly Action _onSave;

        public TodoDetailsSaveButton(Todo todo, Action onSave)
        {
            _onSave = onSave;
            Text = todo.IsDraft ? "Create" : "Save";
            Width = 100;
        }

        protected override void OnClick(MouseEventArgs e)
        {
            _onSave();
            base.OnClick(e);
        }
    }
}