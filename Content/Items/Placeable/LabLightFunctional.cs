using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable
{
    public class LabLightFunctional : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 0;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.createTile = ModContent.TileType<Tiles.Lab.LabLightFunctional>();
        }
    }
}