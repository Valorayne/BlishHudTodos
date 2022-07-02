using System;
using Blish_HUD.Modules.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace Todo_List
{
    public class Resources : IDisposable, TodoCornerIcon.IResources, TodoWindow.IResources
    {
        public Texture2D WindowEmblem { get; }
        public Texture2D WindowBackground { get; set; }
        
        public Texture2D CornerIcon { get; }
        public Texture2D CornerIconHovered { get; }
        
        public Resources(ContentsManager contents)
        {
            WindowEmblem = contents.GetTexture(@"textures\parchment.png");
            WindowBackground = contents.GetTexture(@"textures\155985.png");
            
            CornerIcon = contents.GetTexture(@"textures\icon.png");
            CornerIconHovered = contents.GetTexture(@"textures\icon-active.png");
        }

        public void Dispose()
        {
            WindowEmblem.Dispose();
            WindowBackground.Dispose();
            
            CornerIcon.Dispose();
            CornerIconHovered.Dispose();
        }
    }
}