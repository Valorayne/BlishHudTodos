using Blish_HUD.Modules;
using System;
using System.ComponentModel.Composition;
using Microsoft.Xna.Framework;
using TodoList.Components;

namespace TodoList
{
	[Export(typeof(Module))]
	public class TodoModule : Module
	{
		private Resources _resources;
		private Settings _settings;
		private TodoCornerIcon _cornerIcon;
		private TodoWindow _window;

		[ImportingConstructor]
		public TodoModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { }

		protected override void Initialize()
		{
			_resources = new Resources(ModuleParameters.ContentsManager);
			_settings = new Settings(ModuleParameters.SettingsManager.ModuleSettings);
			_window = new TodoWindow(_resources);
			_cornerIcon = new TodoCornerIcon(_resources, _window.Window, _settings);
		}

		protected override void OnModuleLoaded(EventArgs e)
		{
			_window.Initialize();
			_cornerIcon.Initialize();
			
			// Base handler must be called
			base.OnModuleLoaded(e);
		}

		protected override void Update(GameTime gameTime)
		{
			// GameService.GameIntegration.Gw2Instance.IsInGame && !GameService.Gw2Mumble.UI.IsMapOpen
		}

		protected override void Unload()
		{
			ModuleEntity.DisposeAllEntities();
		}
	}
}
