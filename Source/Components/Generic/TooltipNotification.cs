using Blish_HUD.Common.UI.Views;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Todos.Source.Utils;

namespace Todos.Source.Components.Generic
{
    public static class TooltipNotification
    {
        public static void Spawn(string message, Point location, int durationInMs = 1000)
        {
            var tooltip = new Tooltip(new BasicTooltipView(message));
            tooltip.Show();
            Utility.Delay(() => tooltip.Dispose(), durationInMs);
        }
    }
}