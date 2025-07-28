using ElementsAwoken.Content.Items.Ancient;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.UI.Tooltips
{
    public class EARarity
    {
        public class Rarity12 : ModRarity
        {
            public override Color RarityColor => EAColors.RarityMagenta;
            public override void SetStaticDefaults()
            {
                ItemID.Sets.IsLavaImmuneRegardlessOfRarity[ModContent.ItemType<VoiditeBar>()] = true;
            }
        }
        public class Rarity13 : ModRarity
        {
            public override Color RarityColor => EAColors.RarityDarkRed;
            public override void SetStaticDefaults()
            {
                ItemID.Sets.IsLavaImmuneRegardlessOfRarity[ModContent.ItemType<VoiditeBar>()] = true;
            }
        }
        public class Rarity14 : ModRarity
        {
            public override Color RarityColor => EAColors.RarityDarkBlue;
            public override void SetStaticDefaults()
            {
                ItemID.Sets.IsLavaImmuneRegardlessOfRarity[ModContent.ItemType<VoiditeBar>()] = true;
            }
        }
        public class Rarity15 : ModRarity
        {
            public override Color RarityColor => EAColors.RarityBrightGreen;
            public override void SetStaticDefaults()
            {
                ItemID.Sets.IsLavaImmuneRegardlessOfRarity[ModContent.ItemType<VoiditeBar>()] = true;
            }
        }
        public class Awakened : ModRarity
        {
            public override Color RarityColor => new Color(220, 50, Main.DiscoB);
            public override void SetStaticDefaults()
            {
                ItemID.Sets.IsLavaImmuneRegardlessOfRarity[ModContent.ItemType<VoiditeBar>()] = true;
            }
        }
        public class Mystic : ModRarity
        {
            public override Color RarityColor => EARaritySettings.GetAnimatedItemColor();
            public override void SetStaticDefaults()
            {
                ItemID.Sets.IsLavaImmuneRegardlessOfRarity[ModContent.ItemType<MysticGemstone>()] = true;
            }
        }
    }
}