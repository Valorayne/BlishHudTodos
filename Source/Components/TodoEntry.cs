using Blish_HUD.Controls;

namespace TodoList.Components
{
    public sealed class TodoEntry : FlowPanel
    {
        private readonly Checkbox _checkbox;
        
        public TodoEntry()
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            WidthSizingMode = SizingMode.Fill;

            _checkbox = new Checkbox
            {
                Parent = this, 
                Text = "Test it like beckham",
            };
        }

        protected override void DisposeControl()
        {
            _checkbox.Dispose();
            base.DisposeControl();
        }
    }
}