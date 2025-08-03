using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    public class RegarothTrophy : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 50000;
            Item.rare = 1;
            Item.createTile = ModContent.TileType<Tiles.Trophies.RegarothTrophy>();
            Item.placeStyle = 0;
        }
    }
}