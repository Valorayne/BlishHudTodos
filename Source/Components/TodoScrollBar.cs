using System;
using Blish_HUD.Controls;

namespace TodoList.Components
{
    public class TodoScrollBar
    {
        private readonly Scrollbar _scrollbar;
        
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