using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Edit;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public class TodoSettingsView : View
    {
        private FlowPanel _panel;
        private IDisposable _showWindowOnMap;

        protected override void Build(Container buildPanel)
        {
            _panel = new FlowPanel
            {
                Parent = buildPanel,
                FlowDirection = ControlFlowDirection.SingleTopToBottom,
                Width = buildPanel.Width / 2, 
                Height = buildPanel.Height,
                OuterControlPadding = new Vector2(10, 10)
            }; 
            
            _showWindowOnMap = AddBooleanSetting(_panel, Settings.ShowWindowOnMap, "Show Window on Map", 
                "Whether or not the Todos window should\r\nalso be shown while the map is opened");
            
            base.Build(buildPanel);
        }

        private static IDisposable AddBooleanSetting(Container parent, SettingEntry<bool> setting, string label, string tooltip = null)
        {
            var row = TodoEditRow.For(parent, new Checkbox { Checked = setting.Value }, label, tooltip);
            
            var interactionHandler = new EventHandler<CheckChangedEvent>((sender, e) => setting.Value = e.Checked);
            row.CheckedChanged += interactionHandler;
            
            var settingChangedHandler = new EventHandler<ValueChangedEventArgs<bool>>((sender, e) => row.Checked = e.NewValue);
            setting.SettingChanged += settingChangedHandler;
            
            return new SimpleDisposable(() =>
            {
                row.CheckedChanged -= interactionHandler;
                setting.SettingChanged -= settingChangedHandler;
            });
        }

        protected override void Unload()
        {
            _showWindowOnMap.Dispose();
            _panel.Dispose();
            base.Unload();
        }
    }
}