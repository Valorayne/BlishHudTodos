using Blish_HUD.Controls;
using Blish_HUD.Input;
using Todos.Source.Utils;

namespace Todos.Source.Components
{
    public sealed class TodoCornerIcon : CornerIcon
    {
        private readonly SettingsModel _settings;

        public TodoCornerIcon(SettingsModel settings)
        {
            _settings = settings;
            Icon = Resources.GetTexture(Textures.CornerIcon);
            BasicTooltipText = "Todos";
        }

        protected override void OnClick(MouseEventArgs e)
        {
            _settings.WindowMinimized.Value = false;
            base.OnClick(e);
        }
    }
}