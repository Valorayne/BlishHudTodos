using Blish_HUD.Controls;
using Blish_HUD.Graphics.UI;
using Microsoft.Xna.Framework;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public class TodoSettingsView : View
    {
        private readonly SettingsModel _settings;
        
        private FlowPanel _leftPanel;
        private FlowPanel _rightPanel;

        public TodoSettingsView(SettingsModel settings) => _settings = settings;

        protected override void Build(Container buildPanel)
        {
            _leftPanel = new TodoSettingsLeft(_settings)
            {
                Parent = buildPanel, 
                Width = buildPanel.Width / 2, 
                Height = buildPanel.Height
            };
            
            _rightPanel = new TodoSettingsRight(_settings)
            {
                Parent = buildPanel,
                Width = buildPanel.Width / 2,
                Height = buildPanel.Height,
                Location = new Point(buildPanel.Width / 2, 0)
            };
            
            base.Build(buildPanel);
        }

        protected override void Unload()
        {
            _leftPanel.Dispose();
            _rightPanel.Dispose();
            base.Unload();
        }
    }
}