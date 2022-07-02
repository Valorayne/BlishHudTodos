using System;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TodoList
{
    public class TodoWindow : IDisposable
    {
        public interface IResources
        {
            Texture2D WindowEmblem { get; }
            Texture2D WindowBackground { get; }
        }

        public Container Window { get; }

        public TodoWindow(IResources resources, SettingCollection settings)
        {
            Window = CreateWindow(resources);
        }

        private static Container CreateWindow(IResources resources)
        {
            var windowRec = new Rectangle(-20, 20, 925, 710);
            var contentRec = new Rectangle(windowRec.X + 47, windowRec.Y + 5, windowRec.Width - 5, windowRec.Height - 10);
            return new TabbedWindow2(resources.WindowBackground, windowRec , contentRec)
            {
                Parent = GameService.Graphics.SpriteScreen,
                Title = "Todo List",
                Emblem = resources.WindowEmblem,
                Subtitle = "Notes",
                SavesPosition = true,
                Id = "96ee8ac0-2364-48df-b653-4af5b2fcbfd3"
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