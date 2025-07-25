using ElementsAwoken.Content.Tiles;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Materials
{
    public class VoiditeOre : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.consumable = true;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<Voidite>();
        }
    }
}