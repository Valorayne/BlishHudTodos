using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoCheckbox : Panel
    {
        private readonly Checkbox _checkbox;

        public TodoCheckbox()
        {
            Width = HEADER_HEIGHT;
            Height = HEADER_HEIGHT;
            
            _checkbox = new Checkbox
            {
                Parent = this, 
                Location = new Point(10, 10)
            };
        }

        protected override void DisposeControl()
        {
            _checkbox.Dispose();
            base.DisposeControl();
        }
    }
}