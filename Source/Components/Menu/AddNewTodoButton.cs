using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components.Menu
{
    public class AddNewTodoButton : StandardButton
    {
        public AddNewTodoButton()
        {
            Text = "Add New Todo";
            Location = new Point(Settings.OverlayWidth.Value - 150, Settings.OverlayHeight.Value - 40);
        }
    }
}