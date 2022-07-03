using Blish_HUD.Modules;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Blish_HUD;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using TodoList.Components;
using TodoList.Components.Details;

namespace TodoList
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
			
			_window = TodoListWindow.Create();
			_cornerIcon = new TodoCornerIcon(_window);
			return Task.CompletedTask;
		}

		protected override void OnModuleLoaded(EventArgs e)
		{
			_window.Show();

			Settings.OverlayWidth.SettingChanged += OverlayDimensionsChanged;
			Settings.OverlayHeight.SettingChanged += OverlayDimensionsChanged;
			
			base.OnModuleLoaded(e);
		}

		private void OverlayDimensionsChanged(object target, ValueChangedEventArgs<int> args)
		{
			_window.Dispose();
			_window = TodoListWindow.Create();
			_window.Show();
		}

		protected override void Update(GameTime gameTime)
		{
			// GameService.GameIntegration.Gw2Instance.IsInGame && !GameService.Gw2Mumble.UI.IsMapOpen
		}

		protected override void Unload()
		{
			_window.Dispose();
			_cornerIcon.Dispose();
			
			Settings.OverlayWidth.SettingChanged -= OverlayDimensionsChanged;
			Settings.OverlayHeight.SettingChanged -= OverlayDimensionsChanged;
			Settings.Dispose();
			
			TodoDetailsWindowFactory.Dispose();
			Data.Dispose();
			Resources.Dispose();
		}
	}
}
