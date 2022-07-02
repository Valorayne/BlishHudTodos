using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;

namespace TodoList.Components
{
    public class TodoWindow : ModuleEntity
    {
        public Container Window { get; }

        public TodoWindow(Resources resources)
        {
            Window = RegisterForDisposal(CreateWindow(resources));
        }

        private static Container CreateWindow(Resources resources)
        {
            var windowRec = new Rectangle(-20, 20, 925, 710);
            var contentRec = new Rectangle(windowRec.X + 47, windowRec.Y + 5, windowRec.Width - 5, windowRec.Height - 10);
            return new TabbedWindow2(resources.GetTexture(Textures.WindowBackground), windowRec , contentRec)
            {
                Parent = GameService.Graphics.SpriteScreen,
                Title = "Todo List",
                Emblem = resources.GetTexture(Textures.WindowEmblem),
                SavesPosition = true,
                Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3"
            };
        }

        public void Initialize()
        {
            
        }
    }
}