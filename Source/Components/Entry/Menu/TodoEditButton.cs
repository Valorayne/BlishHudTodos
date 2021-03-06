using Blish_HUD.Controls;
using Todos.Source.Components.Generic;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Menu
{
    public sealed class TodoEditButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;
        
        private readonly TodoModel _todo;

        public TodoEditButton(TodoModel todo) : base(
            Resources.GetTexture(Textures.EditIcon),
            Resources.GetTexture(Textures.EditIconHovered),
            WIDTH, WIDTH,
            _ => todo.IsEditing.Value = !todo.IsEditing.Value)
        {
            _todo = todo;
            OnEditModeChanged(_todo.IsEditing.Value);
            _todo.IsEditing.Changed += OnEditModeChanged;
        }

        private void OnEditModeChanged(bool isInEditMode)
        {
            BasicTooltipText = isInEditMode ? "Stop Editing" : "Edit";
        }

        protected override void DisposeControl()
        {
            _todo.IsEditing.Changed -= OnEditModeChanged;
            base.DisposeControl();
        }
    }
}