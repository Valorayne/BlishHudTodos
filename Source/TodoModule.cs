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
		private Resources _resources;
		private TodoWindow _window;
		private TodoCornerIcon _cornerIcon;

		protected override void DefineSettings(SettingCollection settings)
		{
			_settings = new Settings(settings);
		}

		protected override Task LoadAsync()
		{
			_resources = new Resources(ModuleParameters.ContentsManager);
			_window = new TodoWindow(_resources, _settings);
			//var overlay = new TodoOverlay(resources, settings);
			_cornerIcon = new TodoCornerIcon(_resources, _window, _settings);
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
			_window = new TodoWindow(_resources, _settings);
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
			_resources.Dispose();
			_window.Dispose();
			_cornerIcon.Dispose();
		}
	}
}
