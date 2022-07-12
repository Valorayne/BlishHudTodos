using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Entry.Edit;
using Todos.Source.Components.Generic;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public class TodoSettingsView : View
    {
        private FlowPanel _panel;
        private IDisposable _showWindowOnMap;
        private IDisposable _opacityWhenNotFocussed;
        private IDisposable _alwaysShowWindow;
        private IDisposable _fixatedWindow;

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

            _alwaysShowWindow = AddBooleanSetting(_panel, Settings.AlwaysShowWindow, "Always visible",
                "Whether or not the Todos window should also be shown during\r\ncutscenes, the character selection screen and loading screens");
            _showWindowOnMap = AddBooleanSetting(_panel, Settings.ShowWindowOnMap, "Show on map", 
                "Whether or not the Todos window should\r\nalso be shown while the map is opened");
            _opacityWhenNotFocussed = AddSliderSetting(_panel, Settings.WindowOpacityWhenNotFocussed,
                "Unfocused opacity", "The opacity of the window when you're not currently using it");
            _fixatedWindow = AddBooleanSetting(_panel, Settings.FixatedWindow, "Fixated Window",
                "When fixated, the Todos window can neither be moved nor resized");
            
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
        
        private static IDisposable AddSliderSetting(Container parent, SettingEntry<float> setting, string label, string tooltip = null)
        {
            var row = TodoEditRow.For(parent, new TrackBar { Value = setting.Value, MinValue = 0, MaxValue = 1, SmallStep = true }, label, tooltip);
            
            var interactionHandler = new EventHandler<ValueEventArgs<float>>((sender, e) => setting.Value = e.Value);
            row.ValueChanged += interactionHandler;
            
            var settingChangedHandler = new EventHandler<ValueChangedEventArgs<float>>((sender, e) => row.Value = e.NewValue);
            setting.SettingChanged += settingChangedHandler;
            
            return new SimpleDisposable(() =>
            {
                row.ValueChanged -= interactionHandler;
                setting.SettingChanged -= settingChangedHandler;
            });
        }

        protected override void Unload()
        {
            _alwaysShowWindow.Dispose();
            _showWindowOnMap.Dispose();
            _opacityWhenNotFocussed.Dispose();
            _fixatedWindow.Dispose();
            _panel.Dispose();
            base.Unload();
        }
    }
}