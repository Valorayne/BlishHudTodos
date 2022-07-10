using Blish_HUD.Controls;
using Todos.Source.Models;

namespace Todos.Source.Components.Entry.Edit
{
    public sealed class TodoEditLocalTime : FlowPanel
    {
        public TodoEditLocalTime(Todo todo)
        {
            FlowDirection = ControlFlowDirection.SingleLeftToRight;
            HeightSizingMode = SizingMode.AutoSize;
            WidthSizingMode = SizingMode.AutoSize;
            
            new Dropdown
            {
                Parent = this, 
                SelectedItem =  "Bla",  
                Items = { "Bla" }
            };
        }
    }
}