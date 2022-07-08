using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Blish_HUD.Modules;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using TodoList.Components;

namespace TodoList
{
	[Export(typeof(Module))]
	public class TodoModule : Module
	{
		[ImportingConstructor]
		public TodoModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { }

		private TodoListWindow _window;

		protected override void DefineSettings(SettingCollection settings)
		{
			Settings.Initialize(settings);
		}

		protected override Task LoadAsync()
		{
			// put here in case anything becomes async in the future
			Resources.Initialize(ModuleParameters.ContentsManager);
			Data.Initialize();
			
			_window = new TodoListWindow();
			return Task.CompletedTask;
		}

		protected override void OnModuleLoaded(EventArgs e)
		{
			_window.Show();
			base.OnModuleLoaded(e);
		}

		protected override void Update(GameTime gameTime)
		{
			// GameService.GameIntegration.Gw2Instance.IsInGame && !GameService.Gw2Mumble.UI.IsMapOpen
			TimeService.ProgressTimer(gameTime);
		}

		protected override void Unload()
		{
			_window.Dispose();
			
			Settings.Dispose();

			TimeService.Dispose();
			Data.Dispose();
			Resources.Dispose();
		}
	}
}
