using System.Collections.Generic;
using Blish_HUD.Modules.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace Todos.Source.Utils
{
    public enum Textures
    {
        CornerIcon,
        Empty,
        Header,
        HeaderHovered,
        AddNewIcon,
        EyeIcon,
        EyeIconClosed,
        EditIcon,
        EditIconHovered,
        DeleteIcon,
        DeleteIconHovered,
        CloseIcon,
        CloseIconHovered,
        ScheduleIcon,
        ReorderIcon,
        ReorderIconUp,
        ReorderIconDown,
        CheckboxUnchecked,
        CheckboxChecked,
        CheckboxHovered
    }
    
    public static class Resources
    {
        private static ContentsManager _contents;
        private static IDictionary<Textures, Texture2D> _loadedTextures;

        private static readonly IReadOnlyDictionary<Textures, string> PATHS = new Dictionary<Textures, string>
        {
            { Textures.CornerIcon, @"textures\156701.png"},
            { Textures.Empty, @"textures\empty.png" },
            { Textures.Header, @"textures\1032325.png" },
            { Textures.HeaderHovered, @"textures\1032324.png" },
            { Textures.AddNewIcon, @"textures\157259.png" },
            { Textures.EyeIcon, @"textures\605021.png" },
            { Textures.EyeIconClosed, @"textures\605020.png" },
            { Textures.EditIcon, @"textures\255277.png" },
            { Textures.EditIconHovered, @"textures\255279.png" },
            { Textures.DeleteIcon, @"textures\156674.png" },
            { Textures.DeleteIconHovered, @"textures\156675.png" },
            { Textures.CloseIcon, @"textures\2338895.png" },
            { Textures.CloseIconHovered, @"textures\2338896.png" },
            { Textures.ScheduleIcon, @"textures\784265.png" },
            { Textures.ReorderIcon, @"textures\154963.png" },
            { Textures.ReorderIconUp, @"textures\154962_inverted.png" },
            { Textures.ReorderIconDown, @"textures\154962.png" },
            { Textures.CheckboxUnchecked, @"textures\155921.png" },
            { Textures.CheckboxChecked, @"textures\155919.png" },
            { Textures.CheckboxHovered, @"textures\155923.png" },
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