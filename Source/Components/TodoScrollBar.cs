using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoScrollBar : Scrollbar
    {
        public TodoScrollBar(Container scrollView, int width, int height) : base(scrollView)
        {
            Height = height;
            Location = new Point(width - 10, 0);
        }
    }
}