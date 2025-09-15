using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable.Darkstone
{
    public class DarkstonePlatform : ModItem
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
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.Darkstone.DarkstonePlatform>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ModContent.ItemType<DarkstoneBrick>(), 1);
            recipe.Register();
        }
    }
}