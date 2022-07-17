using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Modules;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Todos.Source.Components;
using Todos.Source.Components.Messages;
using Todos.Source.Persistence;
using Todos.Source.Utils;

namespace Todos.Source
{
	[Export(typeof(Module))]
	public class TodoModule : Module
	{
		private TodoVisualsManager _visuals;
		
		[ImportingConstructor]
		public TodoModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { }

		protected override void DefineSettings(SettingCollection settings)
		{
			Settings.Initialize(settings);
		}

		protected override async Task LoadAsync()
		{
			Resources.Initialize(ModuleParameters.ContentsManager);
			await Data.Initialize(ModuleParameters.DirectoriesManager);
			SaveScheduler.Initialize(ModuleParameters.DirectoriesManager);
			ConfirmDeletionWindow.Initialize();
			_visuals = new TodoVisualsManager();
		}

		protected override void OnModuleLoaded(EventArgs e)
		{
			_visuals.OnModuleLoaded();
			base.OnModuleLoaded(e);
		}

		public override IView GetSettingsView()
		{
			return new TodoSettingsView();
		}

		protected override void Update(GameTime gameTime)
		{
			TimeService.ProgressTimer(gameTime);
			SaveScheduler.Progress(gameTime);
		}
		
		protected override void Unload()
		{
			_visuals.Dispose();
			Data.Dispose();
			Settings.Dispose();

			TimeService.Dispose();
			SaveScheduler.Dispose();
			Resources.Dispose();
			ConfirmDeletionWindow.Dispose();
		}
	}
}
