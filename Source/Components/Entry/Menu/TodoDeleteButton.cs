﻿using System;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Generic;
using Todos.Source.Components.Messages;
using Todos.Source.Models;
using Todos.Source.Utils;

namespace Todos.Source.Components.Entry.Menu
{
    public sealed class TodoDeleteButton : HoverButton
    {
        public const int WIDTH = Panel.HEADER_HEIGHT;
        private readonly PopupModel _popupModel;
        private readonly Action _saveScroll;
        private readonly TodoModel _todo;

        private readonly TodoListModel _todoList;

        public TodoDeleteButton(TodoListModel todoList, TodoModel todo, PopupModel popupModel, Action saveScroll) :
            base(
                Resources.GetTexture(Textures.DeleteIcon),
                Resources.GetTexture(Textures.DeleteIconHovered),
                WIDTH, WIDTH, _ => { }
            )
        {
            _todoList = todoList;
            _todo = todo;
            _popupModel = popupModel;
            _saveScroll = saveScroll;
            BasicTooltipText = "Delete";

            _todoList.MovingTodo.Subscribe(this, move => Utility.Delay(() => Visible = move != todo));
        }

        private bool PopupShown => _popupModel.Parent == Parent;

        protected override void OnClick(MouseEventArgs e)
        {
            if (PopupShown)
                _popupModel.Close();
            else
                OpenPopup();

            base.OnClick(e);
        }

        private void OpenPopup()
        {
            var popup = _popupModel.Open(Parent, new ConfirmDeletionWindow
            {
                OnYes = () =>
                {
                    _saveScroll();
                    _todo.IsDeleted.Value = true;
                    _popupModel.Close();
                },
                OnNo = _popupModel.Close
            });
            popup.Location = new Point(AbsoluteBounds.Center.X - popup.Width, AbsoluteBounds.Center.Y);
        }

        protected override void DisposeControl()
        {
            _todoList.Unsubscribe(this);
            base.DisposeControl();
        }
    }
}