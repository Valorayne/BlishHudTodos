using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using TodoList.Components.Details;

namespace TodoList.Components.Menu
{
    public class AddNewTodoButton : StandardButton
    {
        public AddNewTodoButton()
        {
            Text = "Add New Todo";
            Location = new Point(Settings.OverlayWidth.Value - 150, Settings.OverlayHeight.Value - 40);

            Click += OnButtonClicked;
        }

        private static void OnButtonClicked(object target, MouseEventArgs args)
        {
            TodoDetailsWindowFactory.Show(args.MousePosition);
        }

        protected override void DisposeControl()
        {
            Click -= OnButtonClicked;
            base.DisposeControl();
        }
    }
}