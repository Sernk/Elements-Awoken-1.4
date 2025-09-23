using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Banners
{
    public class VampireBatBanner : ModItem
    {
        public override string Texture => "ElementsAwoken/Content/Items/Banners/GiantVampireBatBanner";
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 24;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.rare = 1;
            Item.value = Item.buyPrice(0, 0, 10, 0);
                        Item.createTile = ModContent.TileType<Tiles.Banners.MonsterBanner>();
            Item.placeStyle = 18;		//Place style means which frame(Horizontally, starting from 0) of the tile should be placed
        }
    }
}