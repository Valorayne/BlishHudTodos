using Blish_HUD;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Models
{
    public class GameModel : ModelBase
    {
        public IProperty<bool> IsMapOpen { get; }
        public IProperty<bool> IsInGame { get; }

        public GameModel()
        {
            IsMapOpen = Add(Variables.FromEvent(GameService.Gw2Mumble.UI.IsMapOpen,
                h => GameService.Gw2Mumble.UI.IsMapOpenChanged += h,
                h => GameService.Gw2Mumble.UI.IsMapOpenChanged -= h));
            IsInGame = Add(Variables.FromEvent(GameService.GameIntegration.Gw2Instance.IsInGame, 
                h => GameService.GameIntegration.Gw2Instance.IsInGameChanged += h,
                h => GameService.GameIntegration.Gw2Instance.IsInGameChanged -= h));
        }
    }
}