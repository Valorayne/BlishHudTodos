using System.Linq;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Menu
{
    public class TodoReorderButton : FlowPanel
    {
        private readonly TodoModel _todo;
        private readonly Panel _upButton;
        private readonly Panel _downButton;

        public TodoReorderButton(TodoModel todo)
        {
            _todo = todo;
            
            Width = HEADER_HEIGHT;
            Height = HEADER_HEIGHT;
            FlowDirection = ControlFlowDirection.SingleTopToBottom;

            _upButton = new Panel { Parent = this, Width = HEADER_HEIGHT, Height = HEADER_HEIGHT / 2 };
            _downButton = new Panel { Parent = this, Width = HEADER_HEIGHT, Height = HEADER_HEIGHT / 2 };

            _upButton.MouseEntered += OnMouseEnteredUp;
            _upButton.Click += OnUpClicked;
            _upButton.MouseLeft += OnMouseLeft;
            
            _downButton.MouseEntered += OnMouseEnteredDown;
            _downButton.Click += OnDownClicked;
            _downButton.MouseLeft += OnMouseLeft;
            
            BackgroundTexture = Resources.GetTexture(Textures.ReorderIcon);
        }

        private bool CanBeMovedUp => Data.VisibleTodos.Value.FirstOrDefault() != _todo;
        private bool CanBeMovedDown => Data.VisibleTodos.Value.LastOrDefault() != _todo;

        private void OnUpClicked(object sender, MouseEventArgs e)
        {
            if (CanBeMovedUp)
                Data.MoveUp(_todo);
        }

        private void OnDownClicked(object sender, MouseEventArgs e)
        {
            if (CanBeMovedDown)
                Data.MoveDown(_todo);
        }

        private void OnMouseEnteredDown(object sender, MouseEventArgs e)
        {
            if (CanBeMovedDown)
            {
                BackgroundTexture = Resources.GetTexture(Textures.ReorderIconDown);
                _downButton.BasicTooltipText = "Move Down";
            }
        }

        private void OnMouseLeft(object sender, MouseEventArgs e)
        {
            if (!_upButton.MouseOver && !_downButton.MouseOver)
            {
                BackgroundTexture = Resources.GetTexture(Textures.ReorderIcon);
                _upButton.BasicTooltipText = "";
                _downButton.BasicTooltipText = "";
            }
        }

        private void OnMouseEnteredUp(object sender, MouseEventArgs e)
        {
            if (CanBeMovedUp)
            {
                BackgroundTexture = Resources.GetTexture(Textures.ReorderIconUp);
                _upButton.BasicTooltipText = "Move Up";
            }
        }

        protected override void DisposeControl()
        {
            _upButton.MouseEntered -= OnMouseEnteredUp;
            _upButton.MouseLeft -= OnMouseLeft;
            
            _downButton.MouseEntered -= OnMouseEnteredDown;
            _downButton.MouseLeft -= OnMouseLeft;
            base.DisposeControl();
        }
    }
}