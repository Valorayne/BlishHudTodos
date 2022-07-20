using System;
using System.Collections.Generic;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;

namespace Todos.Source.Components.Settings
{
    public class TodoSettingsRight : FlowPanel
    {
        private readonly List<IDisposable> _rows;

        public TodoSettingsRight(SettingsModel settings)
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = new Vector2(10, 10);

            _rows = new List<IDisposable>
            {
                SettingRow.Keybinding(this, settings.ToggleWindowHotkey, "Show/Hide Window",
                    "Maximizes or minimizes the Todos window")
            };
        }

        protected override void DisposeControl()
        {
            foreach (var row in _rows)
                row.Dispose();
            _rows.Clear();
            base.DisposeControl();
        }
    }
}