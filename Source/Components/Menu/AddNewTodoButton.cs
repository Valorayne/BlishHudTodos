﻿using Blish_HUD.Controls;
using Blish_HUD.Input;
using TodoList.Models;

namespace TodoList.Components.Menu
{
    public class AddNewTodoButton : Image
    {
        public AddNewTodoButton() : base(Resources.GetTexture(Textures.AddNewIcon))
        {
            Width = 40;
            Height = 40;
            BasicTooltipText = "Add new Todo";
        }

        protected override void OnClick(MouseEventArgs e)
        {
            Data.Add(new Todo());
            base.OnClick(e);
        }
    }
}