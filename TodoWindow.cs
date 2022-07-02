using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Todo_List
{
    public class TodoWindow : IDisposable
    {
        public interface IResources
        {
            Texture2D WindowEmblem { get; }
            Texture2D WindowBackground { get; }
        }

        public TabbedWindow2 Window { get; }

        public TodoWindow(IResources resources)
        {
            var windowRec = new Rectangle(-20, 20, 925, 710);
            var contentRec = new Rectangle(windowRec.X + 47, windowRec.Y + 5, windowRec.Width - 5, windowRec.Height - 10);
            Window = new TabbedWindow2(resources.WindowBackground, windowRec , contentRec)
            {
                Parent = GameService.Graphics.SpriteScreen,
                Title = "Todo List",
                Emblem = resources.WindowEmblem,
                Subtitle = "Notes",
                SavesPosition = true,
                Id = "NotesModule_995A840DE116AB0805655673E1C4930851149861252974B00F9DE4ACEF762578"
            };
        }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            Window.Dispose();
        }
    }
}