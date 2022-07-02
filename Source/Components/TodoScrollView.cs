using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoScrollView
    {
        public FlowPanel View { get; }
        
        public TodoScrollView(Container parent, Resources resources, Settings settings)
        {
            View = new FlowPanel
            {
                Parent = parent,
                FlowDirection = ControlFlowDirection.SingleTopToBottom,
                WidthSizingMode = SizingMode.Fill,
                HeightSizingMode = SizingMode.Fill, 
                BackgroundColor = Color.Aqua
            };
        }
    }
}