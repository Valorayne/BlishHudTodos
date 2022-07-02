using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public sealed class TodoScrollView : FlowPanel
    {
        public TodoScrollView(Resources resources, Settings settings)
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;

            for (var i = 0; i < 50; i++)
            {
                new Label()
                {
                    Parent = this,
                    Text = $"Entry Number {i}"
                };
            }
        }
    }
}