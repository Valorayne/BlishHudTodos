using Blish_HUD.Controls;

namespace TodoList.Components
{
    public class TodoScrollBar
    {
        public TodoScrollBar(Container parent, Container scrollView)
        {
            var scrollbar = new Scrollbar(scrollView)
            {
                Parent = parent,
                Right = 0,
                Height = scrollView.Height
            };
        }
    }
}