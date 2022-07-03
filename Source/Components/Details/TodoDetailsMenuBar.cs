using Blish_HUD.Controls;
using TodoList.Models;

namespace TodoList.Components.Details
{
    public sealed class TodoDetailsMenuBar : FlowPanel
    {
        public const int PADDING = 5;

        public StandardButton SaveButton { get; }
        public StandardButton DeleteButton { get; }

        public TodoDetailsMenuBar(Todo todo)
        {
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.AutoSize;
            FlowDirection = ControlFlowDirection.RightToLeft;
            Padding = new Thickness(PADDING);

            SaveButton = new StandardButton
            {
                Parent = this,
                Text = todo.IsDraft ? "Create" : "Save",
                Width = 100
            };
            
            if (!todo.IsDraft)
                DeleteButton = new StandardButton
                {
                    Parent = this,
                    Text = "Delete",
                    Width = 100
                };
        }

        protected override void DisposeControl()
        {
            SaveButton.Dispose();
            DeleteButton?.Dispose();
            base.DisposeControl();
        }
    }
}