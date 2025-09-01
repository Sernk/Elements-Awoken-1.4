using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Blightfire
{
    public class Blightfire : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 11;
        }
    }
}