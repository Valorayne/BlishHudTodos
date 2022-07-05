using System.Collections.Generic;
using Blish_HUD.Modules.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace TodoList
{
    public enum Textures
    {
        CornerIcon,
        CornerIconHovered,
        Empty,
        Header,
        HeaderHovered,
        CollapseArrowDown, 
        CollapseArrowRight,
        AddNewIcon,
        EyeIcon,
        EyeIconClosed,
        EditIcon,
        EditIconHovered,
        DeleteIcon,
        DeleteIconHovered
    }
    
    public static class Resources
    {
        private static ContentsManager _contents;
        private static IDictionary<Textures, Texture2D> _loadedTextures;

        private static readonly IReadOnlyDictionary<Textures, string> PATHS = new Dictionary<Textures, string>
        {
            { Textures.CornerIcon, @"textures\156701.png"},
            { Textures.CornerIconHovered, @"textures\156702.png"},
            { Textures.Empty, @"textures\empty.png" },
            { Textures.Header, @"textures\1032325.png" },
            { Textures.HeaderHovered, @"textures\1032324.png" },
            { Textures.CollapseArrowDown, @"textures\155953.png" },
            { Textures.CollapseArrowRight, @"textures\155953_rotated.png" },
            { Textures.AddNewIcon, @"textures\155914.png" },
            { Textures.EyeIcon, @"textures\605021.png" },
            { Textures.EyeIconClosed, @"textures\605020.png" },
            { Textures.EditIcon, @"textures\104168.png" },
            { Textures.EditIconHovered, @"textures\104168_hovered.png" },
            { Textures.DeleteIcon, @"textures\156012.png" },
            { Textures.DeleteIconHovered, @"textures\156012.png" },
        };
        
        public static Texture2D GetTexture(Textures texture)
        {
            if (!_loadedTextures.ContainsKey(texture))
                _loadedTextures.Add(texture, _contents.GetTexture(PATHS[texture]));
            return _loadedTextures[texture];
        }
        
        public static void Initialize(ContentsManager contents)
        {
            _contents = contents;
            _loadedTextures = new Dictionary<Textures, Texture2D>();
        }

        public static void Dispose()
        {
            foreach (var texture in _loadedTextures.Values)
                texture.Dispose();
            _loadedTextures.Clear();
            _loadedTextures = null;
            _contents = null;
        }
    }
}