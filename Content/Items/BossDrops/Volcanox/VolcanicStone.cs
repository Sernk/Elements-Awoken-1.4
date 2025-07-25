using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Volcanox
{
    public class VolcanicStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 2, 50, 0);
            Item.rare = 11;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Volcanic Stone");
            // Tooltip.SetDefault("The rocks seem to constantly shatter into flames");
        }
    }
}
