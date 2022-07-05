using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Components.Details;

namespace TodoList.Components.Menu
{
    public class AddNewTodoButton : Image
    {
        public AddNewTodoButton() : base(Resources.GetTexture(Textures.AddNewIcon))
        {
            Width = 40;
            Height = 40;
            BasicTooltipText = "Add new Todo";
            Click += OnButtonClicked;
        }

        private static void OnButtonClicked(object target, MouseEventArgs args)
        {
            TodoDetailsWindowPool.Spawn(args.MousePosition);
        }

        protected override void DisposeControl()
        {
            Click -= OnButtonClicked;
            base.DisposeControl();
        }
    }
}