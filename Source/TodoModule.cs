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
		[ImportingConstructor]
		public TodoModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { }

		protected override void Initialize()
		{
			var resources = new Resources(ModuleParameters.ContentsManager);
			var settings = new Settings(ModuleParameters.SettingsManager.ModuleSettings);
			var window = new TodoWindow(resources, settings);
			//var overlay = new TodoOverlay(resources, settings);
			var cornerIcon = new TodoCornerIcon(resources, window.Window, settings);
		}

		protected override void OnModuleLoaded(EventArgs e)
		{
			ModuleEntity.InitializeAllEntities();
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
