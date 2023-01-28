using System.Collections.Generic;
using Blish_HUD.Modules.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace Todos.Source.Utils
{
    public enum Textures
    {
        CornerIcon,
        CornerIconAlert,
        Empty,
        Header,
        HeaderHovered,
        AddNewIcon,
        EyeIcon,
        EyeIconClosed,
        LockIcon,
        LockIconActive,
        EditIcon,
        EditIconHovered,
        DeleteIcon,
        DeleteIconHovered,
        CloseIcon,
        CloseIconHovered,
        ScheduleIcon,
        ReorderIcon,

        // Checkboxes
        CheckboxUncheckedStandard,
        CheckboxCheckedStandard,
        CheckboxHoveredStandard,

        CheckboxUncheckedGem,
        CheckboxCheckedGem,
        CheckboxHoveredGem,

        CheckboxUncheckedMastery,
        CheckboxCheckedMastery,
        CheckboxHoveredMastery,

        CheckboxUncheckedSquared,
        CheckboxCheckedSquared,
        CheckboxHoveredSquared,

        CheckboxUncheckedRound,
        CheckboxCheckedRound,
        CheckboxHoveredRound,

        CheckboxUncheckedCheckmark,
        CheckboxCheckedCheckmark,
        CheckboxHoveredCheckmark
    }

    public static class Resources
    {
        private static ContentsManager _contents;
        private static IDictionary<Textures, Texture2D> _loadedTextures;

        private static readonly IReadOnlyDictionary<Textures, string> PATHS = new Dictionary<Textures, string>
        {
            { Textures.CornerIcon, @"textures\156701.png" },
            { Textures.CornerIconAlert, @"textures\156702_red.png" },
            { Textures.Empty, @"textures\empty.png" },
            { Textures.Header, @"textures\1032325.png" },
            { Textures.HeaderHovered, @"textures\1032324.png" },
            { Textures.AddNewIcon, @"textures\157259.png" },
            { Textures.EyeIcon, @"textures\605021.png" },
            { Textures.EyeIconClosed, @"textures\605020.png" },
            { Textures.LockIcon, @"textures\2208335.png" },
            { Textures.LockIconActive, @"textures\547833.png" },
            { Textures.EditIcon, @"textures\255277.png" },
            { Textures.EditIconHovered, @"textures\255279.png" },
            { Textures.DeleteIcon, @"textures\156674.png" },
            { Textures.DeleteIconHovered, @"textures\156675.png" },
            { Textures.CloseIcon, @"textures\2338895.png" },
            { Textures.CloseIconHovered, @"textures\2338896.png" },
            { Textures.ScheduleIcon, @"textures\784265.png" },
            { Textures.ReorderIcon, @"textures\605018.png" },

            { Textures.CheckboxUncheckedStandard, @"textures\checkbox\155921.png" },
            { Textures.CheckboxCheckedStandard, @"textures\checkbox\155919.png" },
            { Textures.CheckboxHoveredStandard, @"textures\checkbox\155923.png" },
            { Textures.CheckboxUncheckedGem, @"textures\checkbox\155090.png" },
            { Textures.CheckboxCheckedGem, @"textures\checkbox\1466385.png" },
            { Textures.CheckboxHoveredGem, @"textures\checkbox\156367.png" },
            { Textures.CheckboxUncheckedMastery, @"textures\checkbox\1078541.png" },
            { Textures.CheckboxCheckedMastery, @"textures\checkbox\1078543.png" },
            { Textures.CheckboxHoveredMastery, @"textures\checkbox\1078542.png" },
            { Textures.CheckboxUncheckedSquared, @"textures\checkbox\2208343.png" },
            { Textures.CheckboxCheckedSquared, @"textures\checkbox\2208341.png" },
            { Textures.CheckboxHoveredSquared, @"textures\checkbox\2208342.png" },
            { Textures.CheckboxUncheckedRound, @"textures\checkbox\965768.png" },
            { Textures.CheckboxCheckedRound, @"textures\checkbox\965769.png" },
            { Textures.CheckboxHoveredRound, @"textures\checkbox\1032331.png" },
            { Textures.CheckboxUncheckedCheckmark, @"textures\checkbox\2572079.png" },
            { Textures.CheckboxCheckedCheckmark, @"textures\checkbox\784259.png" },
            { Textures.CheckboxHoveredCheckmark, @"textures\checkbox\1032331.png" }
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