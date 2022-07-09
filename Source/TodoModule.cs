using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Blish_HUD;
using Blish_HUD.Modules;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Todos.Source.Components;
using Todos.Source.Components.Messages;
using Todos.Source.Utils;

namespace Todos.Source
{
	[Export(typeof(Module))]
	public class TodoModule : Module
	{
		[ImportingConstructor]
		public TodoModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { }

		private TodoListWindow _window;
		private TodoCornerIcon _cornerIcon;

		protected override void DefineSettings(SettingCollection settings)
		{
			Settings.Initialize(settings);
		}

		protected override Task LoadAsync()
		{
			// put here in case anything becomes async in the future
			Resources.Initialize(ModuleParameters.ContentsManager);
			Data.Initialize();
			
			if (Settings.WindowShown.Value)
				_window = new TodoListWindow();
			else 
				_cornerIcon = new TodoCornerIcon();
			
			return Task.CompletedTask;
		}

		protected override void OnModuleLoaded(EventArgs e)
		{
			_window?.Show();

			Settings.WindowShown.SettingChanged += OnWindowVisibilityChanged;
			
			base.OnModuleLoaded(e);
		}

		private void OnWindowVisibilityChanged(object sender, ValueChangedEventArgs<bool> e)
		{
			if (e.NewValue)
			{
				_cornerIcon.Dispose();
				_window = new TodoListWindow();
				_window.Show();
			}
			else
			{
				_window.Dispose();
				_cornerIcon = new TodoCornerIcon();
			}
		}

		protected override void Update(GameTime gameTime)
		{
			// GameService.GameIntegration.Gw2Instance.IsInGame && !GameService.Gw2Mumble.UI.IsMapOpen
			TimeService.ProgressTimer(gameTime);
		}

		protected override void Unload()
		{
			Settings.WindowShown.SettingChanged -= OnWindowVisibilityChanged;
			 
			_window?.Dispose();
			_cornerIcon?.Dispose();
			
			Settings.Dispose();

			TimeService.Dispose();
			Data.Dispose();
			Resources.Dispose();
			ConfirmDeletionWindow.Dispose();
		}
	}
}
