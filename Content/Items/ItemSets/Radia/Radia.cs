using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Radia
{
    public class Radia : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ModContent.RarityType<EARarity.Rarity13>();
        }
    }
}