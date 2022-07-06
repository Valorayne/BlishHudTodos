using Blish_HUD.Controls;
using TodoList.Components.Menu;

namespace TodoList.Components
{
    public sealed class TodoListPanel : FlowPanel
    {
        public TodoListPanel()
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            WidthSizingMode = SizingMode.Fill;
            HeightSizingMode = SizingMode.Fill;
            
            new TodoListMenuBar { Parent = this };
            new TodoScrollView { Parent = this };
        }
    }
}