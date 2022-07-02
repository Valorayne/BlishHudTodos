using Blish_HUD;
using Blish_HUD.Modules;
using Blish_HUD.Settings;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Todo_List
{
	[Export(typeof(Module))]
	public class TodoModule : Module
	{
		public static readonly Logger Logger = Logger.GetLogger<TodoModule>();
		
		private Resources _resources;
		private TodoCornerIcon _cornerIcon;
		private TodoWindow _window;

		[ImportingConstructor]
		public TodoModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { }

		protected override void DefineSettings(SettingCollection settings)
		{
		}

		protected override void Initialize()
		{
			_resources = new Resources(ModuleParameters.ContentsManager);
			_window = new TodoWindow(_resources);
			_cornerIcon = new TodoCornerIcon(_resources, _window.Window);
		}

		protected override async Task LoadAsync()
		{

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
			_window.Dispose();
			_cornerIcon.Dispose();
			_resources.Dispose();
		}

	}

}
