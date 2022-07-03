using Blish_HUD.Modules;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Blish_HUD;
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

		private Settings _settings;
		private TodoWindow _window;
		private TodoCornerIcon _cornerIcon;

		protected override void DefineSettings(SettingCollection settings)
		{
			_settings = new Settings(settings);
		}

		protected override Task LoadAsync()
		{
			// put here in case anything becomes async in the future
			Resources.Initialize(ModuleParameters.ContentsManager);
			_window = TodoWindow.Create(_settings);
			_cornerIcon = new TodoCornerIcon(_window, _settings);
			return Task.CompletedTask;
		}

		protected override void OnModuleLoaded(EventArgs e)
		{
			_window.Show();

			_settings.OverlayWidth.SettingChanged += OverlayDimensionsChanged;
			_settings.OverlayHeight.SettingChanged += OverlayDimensionsChanged;
			
			base.OnModuleLoaded(e);
		}

		private void OverlayDimensionsChanged(object target, ValueChangedEventArgs<int> args)
		{
			_window.Dispose();
			_window = TodoWindow.Create(_settings);
			_window.Show();
		}

		protected override void Update(GameTime gameTime)
		{
			// GameService.GameIntegration.Gw2Instance.IsInGame && !GameService.Gw2Mumble.UI.IsMapOpen
		}

		protected override void Unload()
		{
			_settings.OverlayWidth.SettingChanged -= OverlayDimensionsChanged;
			_settings.OverlayHeight.SettingChanged -= OverlayDimensionsChanged;
			Resources.Dispose();
			_window.Dispose();
			_cornerIcon.Dispose();
		}
	}
}
