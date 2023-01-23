using System;
using System.Collections.Generic;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Models;

namespace Todos.Source.Components.Settings
{
    public class TodoSettingsLeft : FlowPanel
    {
        private readonly List<IDisposable> _rows;

        public TodoSettingsLeft(SettingsModel settings)
        {
            FlowDirection = ControlFlowDirection.SingleTopToBottom;
            OuterControlPadding = new Vector2(10, 10);

            _rows = new List<IDisposable>
            {
                SettingRow.Boolean(this, settings.ShowWindowOutOfGame, "Show outside of game",
                    "Whether or not the Todos window should also be shown during\r\ncutscenes, the character selection screen and loading screens"),
                SettingRow.Boolean(this, settings.ShowWindowOnMap, "Show on map",
                    "Whether or not the Todos window should\r\nalso be shown while the map is opened"),
                SettingRow.Boolean(this, settings.FixatedWindow, "Fixed Window",
                    "When fixed, the Todos window can neither be moved nor resized"),
                SettingRow.Boolean(this, settings.ClickThroughBackground, "Click Through Background",
                    "Whether or not it should be possible to click through the\r\nbackground to interact with game elements (e.g. Daily Chests)"),
                SettingRow.Slider(this, settings.BackgroundOpacity,
                    "Background opacity", "The opacity of the window background"),
                SettingRow.Slider(this, settings.WindowOpacityWhenNotFocussed,
                    "Unfocused opacity", "The opacity of the window when you're not currently using it"),
                SettingRow.Dropdown(this, settings.CheckboxType, "Checkbox Type",
                    "The visual appearance of the checkboxes of todo entries")
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