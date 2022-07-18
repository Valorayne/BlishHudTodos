using System;
using Microsoft.Xna.Framework.Graphics;

namespace Todos.Source.Utils
{
    public enum CheckboxType
    {
        Standard = 0,
        Gem = 1,
        Mastery = 2,
        Squared = 3, 
        Round = 4,
        Checkmark = 5
    }

    public static class CheckboxTypeExtensions
    {
        public static Texture2D GetBackgroundImage(this CheckboxType type)
        {
            switch (type)
            {
                case CheckboxType.Standard: return Resources.GetTexture(Textures.CheckboxUncheckedStandard);
                case CheckboxType.Gem: return Resources.GetTexture(Textures.CheckboxUncheckedGem);
                case CheckboxType.Mastery: return Resources.GetTexture(Textures.CheckboxUncheckedMastery);
                case CheckboxType.Squared: return Resources.GetTexture(Textures.CheckboxUncheckedSquared);
                case CheckboxType.Round: return Resources.GetTexture(Textures.CheckboxUncheckedRound);
                case CheckboxType.Checkmark: return Resources.GetTexture(Textures.CheckboxUncheckedCheckmark);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        public static Texture2D GetHoveredImage(this CheckboxType type)
        {
            switch (type)
            {
                case CheckboxType.Standard: return Resources.GetTexture(Textures.CheckboxHoveredStandard);
                case CheckboxType.Gem: return Resources.GetTexture(Textures.CheckboxHoveredGem);
                case CheckboxType.Mastery: return Resources.GetTexture(Textures.CheckboxHoveredMastery);
                case CheckboxType.Squared: return Resources.GetTexture(Textures.CheckboxHoveredSquared);
                case CheckboxType.Round: return Resources.GetTexture(Textures.CheckboxHoveredRound);
                case CheckboxType.Checkmark: return Resources.GetTexture(Textures.CheckboxHoveredCheckmark);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        public static Texture2D GetCheckedImage(this CheckboxType type)
        {
            switch (type)
            {
                case CheckboxType.Standard: return Resources.GetTexture(Textures.CheckboxCheckedStandard);
                case CheckboxType.Gem: return Resources.GetTexture(Textures.CheckboxCheckedGem);
                case CheckboxType.Mastery: return Resources.GetTexture(Textures.CheckboxCheckedMastery);
                case CheckboxType.Squared: return Resources.GetTexture(Textures.CheckboxCheckedSquared);
                case CheckboxType.Round: return Resources.GetTexture(Textures.CheckboxCheckedRound);
                case CheckboxType.Checkmark: return Resources.GetTexture(Textures.CheckboxCheckedCheckmark);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}