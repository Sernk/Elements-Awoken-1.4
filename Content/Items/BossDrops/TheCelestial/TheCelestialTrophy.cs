using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheCelestial
{
    public class TheCelestialTrophy : ModItem
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
            Item.value = 50000;
            Item.rare = 1;
            Item.createTile = ModContent.TileType<Tiles.Trophies.TheCelestialTrophy>();
            Item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Celestial Trophy");
        }

    }
}
