using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Wasteland
{
    public class WastelandTrophy : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 1;
            Item.createTile = ModContent.TileType<Tiles.Trophies.WastelandTrophy>();
            Item.placeStyle = 0;
        }
    }
}