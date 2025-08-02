using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Azana
{
    public class AzanaTrophy : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 1;
            Item.createTile = ModContent.TileType<Tiles.Trophies.AzanaTrophy>();
            Item.placeStyle = 0;
        }
    }
}