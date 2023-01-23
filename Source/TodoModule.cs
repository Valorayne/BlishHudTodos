using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Modules;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Todos.Source.Components;
using Todos.Source.Components.Settings;
using Todos.Source.Models;
using Todos.Source.Persistence;
using Todos.Source.Utils;
using Todos.Source.Utils.Reactive;

namespace Todos.Source
{
    [Export(typeof(Module))]
    public class TodoModule : Module
    {
        private TodoCornerIcon _cornerIcon;
        private GameModel _game;
        private PopupModel _popup;
        private SettingsModel _settings;
        private TodoListModel _todoList;
        private TodoVisualsManager _visuals;

        [ImportingConstructor]
        public TodoModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters)
        {
        }

        protected override void DefineSettings(SettingCollection settings)
        {
            _settings = new SettingsModel(settings);
        }

        public override IView GetSettingsView()
        {
            return new TodoSettingsView(_settings);
        }

        protected override async Task LoadAsync()
        {
            Resources.Initialize(ModuleParameters.ContentsManager);
            MouseService.Initialize();
            _game = new GameModel();
            _popup = new PopupModel();
            _todoList = await TodoListModel.Initialize(_settings, ModuleParameters.DirectoriesManager);
            SaveScheduler.Initialize(ModuleParameters.DirectoriesManager);
            _cornerIcon = new TodoCornerIcon(_settings);
            _visuals = new TodoVisualsManager(_settings, _game, _todoList, _popup);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseService.Update();
            TimeService.ProgressTimer(gameTime);
            SaveScheduler.Progress(gameTime);
        }

        protected override void Unload()
        {
            _visuals?.Dispose();
            _cornerIcon?.Dispose();
            _popup?.Dispose();
            _todoList?.Dispose();

            TimeService.Dispose();
            SaveScheduler.Dispose();

            MouseService.Dispose();
            Resources.Dispose();
            _settings?.Dispose();
            _game?.Dispose();

            if (Debug.TrackVariableDisposals)
                Variable<object>.CheckForNotDisposedVariables();
        }
    }
}