using Terraria;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.BossDrops.Ancients
{
    public class CrystalAmalgamate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.rare = ModContent.RarityType<Rarity14>();
            Item.value = Item.sellPrice(0, 20, 0, 0);
        }
    }
}