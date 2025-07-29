using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories.Teeth
{
    public class TombCrawlerTooth : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = 1;    
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pickSpeed -= 0.25f;
        }
    }
}