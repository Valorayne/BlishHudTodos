using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoCheckbox : Panel
    {
        private readonly Point SIZE = new Point(32, 32);
        private readonly Point OFFSET = new Point(2, 2);
        
        private readonly TodoModel _todo;
        private readonly Image _hovered;
        private readonly Image _checked;

        public TodoCheckbox(TodoModel todo)
        {
            _todo = todo;
            
            Width = HEADER_HEIGHT;
            Height = HEADER_HEIGHT;

            new Image(Resources.GetTexture(Textures.CheckboxUnchecked)) { Parent = this, Location = OFFSET, Size = SIZE };
            _hovered = new Image(Resources.GetTexture(Textures.CheckboxHovered)) { Parent = this, Location = OFFSET, Size = SIZE, Visible = false };
            _checked = new Image(Resources.GetTexture(Textures.CheckboxChecked))
            {
                Parent = this, 
                Location = OFFSET, 
                Size = SIZE, 
                Visible = todo.Done, 
                BasicTooltipText = GetTooltipText(todo)
            };
            
            todo.DoneChanged += OnDoneChanged;
            TimeService.NewMinute += CheckForChange;
        }

        protected override void OnMouseEntered(MouseEventArgs e)
        {
            _hovered.Visible = true;
            base.OnMouseEntered(e);
        }

        protected override void OnMouseLeft(MouseEventArgs e)
        {
            _hovered.Visible = false;
            base.OnMouseLeft(e);
        }

        private void OnDoneChanged(bool newDone)
        {
            UpdateState();
        }

        private void CheckForChange(object sender, GameTime e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            _checked.Visible = _todo.Done;
            _checked.BasicTooltipText = GetTooltipText(_todo);
        }

        protected override void OnClick(MouseEventArgs e)
        {
            _todo.Done = !_todo.Done;
            base.OnClick(e);
        }

        private string GetTooltipText(TodoModel todo)
        {
            return todo.Done
                ? $"Done: {todo.LastExecution?.ToDaysSinceString()}, {_todo.LastExecution?.ToShortTimeString()}" 
                : null;
        }

        protected override void DisposeControl()
        {
            _todo.DoneChanged -= OnDoneChanged;
            TimeService.NewMinute -= CheckForChange;
            base.DisposeControl();
        }
    }
}