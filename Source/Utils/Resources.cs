using System.Collections.Generic;
using Blish_HUD.Modules.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace TodoList
{
    public enum Textures
    {
        WindowEmblem,
        WindowBackground,
        CornerIcon,
        CornerIconHovered,
        Empty
    }
    
    public class Resources : ModuleEntity
    {
        private readonly ContentsManager _contents;
        private readonly IDictionary<Textures, Texture2D> _loadedTextures = new Dictionary<Textures, Texture2D>();

        private readonly IDictionary<Textures, string> _texturePaths = new Dictionary<Textures, string>
        {
            { Textures.CornerIcon, @"textures\icon.png"},
            { Textures.CornerIconHovered, @"textures\icon-active.png"},
            { Textures.WindowBackground, @"textures\155985.png" },
            { Textures.WindowEmblem, @"textures\parchment.png" },
            { Textures.Empty, @"textures\empty.png" }
        };
        
        public Texture2D GetTexture(Textures texture)
        {
            if (!_loadedTextures.ContainsKey(texture))
                _loadedTextures.Add(texture, RegisterForDisposal(_contents.GetTexture(_texturePaths[texture])));
            return _loadedTextures[texture];
        }
        
        public Resources(ContentsManager contents)
        {
            _contents = contents;
        }

        protected override void CustomDispose()
        {
            _loadedTextures.Clear();
        }
    }
}