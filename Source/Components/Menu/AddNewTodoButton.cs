using Blish_HUD.Controls;
using Blish_HUD.Input;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Menu
{
    public class AddNewTodoButton : Image
    {
        private readonly TodoListModel _todoList;

        public AddNewTodoButton(TodoListModel todoList) : base(Resources.GetTexture(Textures.AddNewIcon))
        {
            _todoList = todoList;
            Width = 40;
            Height = 40;
            BasicTooltipText = "Add new Todo";
        }

        protected override void OnClick(MouseEventArgs e)
        {
            _todoList.AddNewTodo();
            base.OnClick(e);
        }
    }
}