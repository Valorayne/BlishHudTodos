using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Todos.Source.Components.Generic;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Components.Settings
{
    public static class SettingRow
    {
        public static IDisposable Slider(Container parent, IVariable<float> setting, string label, string tooltip = null)
        {
            var row = TodoInputRow.For(parent, new TrackBar { Value = setting.Value, MinValue = 0, MaxValue = 1, SmallStep = true }, label, tooltip);
            
            var interactionHandler = new EventHandler<ValueEventArgs<float>>((sender, e) => setting.Value = e.Value);
            row.ValueChanged += interactionHandler;
            
            setting.Subscribe(label, newValue => row.Value = newValue);
            
            return new SimpleDisposable(() =>
            {
                row.ValueChanged -= interactionHandler;
                setting.Unsubscribe(label);
            });
        }
        
        public static IDisposable Keybinding(Container parent, IVariable<KeyBinding> setting, string label, string tooltip = null)
        {
            var row = new KeybindingAssigner(setting.Value) { Parent = parent, KeyBindingName = label, BasicTooltipText = tooltip };
            
            var interactionHandler = new EventHandler<EventArgs>((sender, e) => setting.Value = row.KeyBinding);
            row.BindingChanged += interactionHandler;
            
            setting.Subscribe(label, newValue => row.KeyBinding = newValue);
            
            return new SimpleDisposable(() =>
            {
                row.BindingChanged -= interactionHandler;
                setting.Unsubscribe(label);
            });
        }
        
        public static IDisposable Boolean(Container parent, IVariable<bool> setting, string label, string tooltip = null)
        {
            var row = TodoInputRow.For(parent, new Checkbox { Checked = setting.Value }, label, tooltip);
            
            var interactionHandler = new EventHandler<CheckChangedEvent>((sender, e) => setting.Value = e.Checked);
            row.CheckedChanged += interactionHandler;
            
            setting.Subscribe(label, newValue => row.Checked = newValue);
            
            return new SimpleDisposable(() =>
            {
                row.CheckedChanged -= interactionHandler;
                setting.Unsubscribe(label);
            });
        }
        
        public static IDisposable Dropdown<T>(Container parent, IVariable<T> setting, string label, string tooltip = null) where T : Enum
        {
            var row = TodoInputRow.For(parent, new Dropdown { SelectedItem = setting.Value.ToString()}, label, tooltip);
            foreach (var name in Enum.GetNames(typeof(T)))
                row.Items.Add(name);
            
            var interactionHandler = new EventHandler<ValueChangedEventArgs>((sender, e) => setting.Value = (T) Enum.Parse(typeof(T), e.CurrentValue));
            row.ValueChanged += interactionHandler;
            
            setting.Subscribe(label, newValue => row.SelectedItem = newValue.ToString());
            
            return new SimpleDisposable(() =>
            {
                row.ValueChanged -= interactionHandler;
                setting.Unsubscribe(label);
            });
        }
    }
}