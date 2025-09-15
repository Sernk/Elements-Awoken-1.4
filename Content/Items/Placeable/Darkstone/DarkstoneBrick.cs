using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable.Darkstone
{
    public class DarkstoneBrick : ModItem
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
            Item.createTile = ModContent.TileType<Tiles.Darkstone.DarkstoneBrick>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Darkstone>(), 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DarkstonePlatform>(), 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}