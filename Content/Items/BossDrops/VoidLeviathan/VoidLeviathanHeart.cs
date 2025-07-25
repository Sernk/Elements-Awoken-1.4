using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.VoidLeviathan
{
    public class VoidLeviathanHeart : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Leviathan Heart");
            // Tooltip.SetDefault("It glows with power");
        }
    }
}
