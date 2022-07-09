using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Blish_HUD;
using Blish_HUD.Modules;
using Blish_HUD.Settings;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xna.Framework;
using Todos.Source.Components.Messages;
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

		protected override Task LoadAsync()
		{
			// put here in case anything becomes async in the future
			Resources.Initialize(ModuleParameters.ContentsManager);
			Data.Initialize();

			_visuals = new TodoVisualsManager();
			
			return Task.CompletedTask;
		}

		protected override void OnModuleLoaded(EventArgs e)
		{
			_visuals.OnModuleLoaded();
			base.OnModuleLoaded(e);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GameService.Gw2Mumble.UI.IsMapOpen)
			{
				var forward = 3;
			}
			TimeService.ProgressTimer(gameTime);
		}

		private bool IsInCharacterLoadingScreen => !GameService.Gw2Mumble.PlayerCharacter.Name.IsNullOrEmpty();
		
		protected override void Unload()
		{
			_visuals.Dispose();
			Settings.Dispose();

			TimeService.Dispose();
			Data.Dispose();
			Resources.Dispose();
			ConfirmDeletionWindow.Dispose();
		}
	}
}
