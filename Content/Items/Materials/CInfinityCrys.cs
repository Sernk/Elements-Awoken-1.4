using ElementsAwoken.Content.Tiles.Crafting;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Materials
{
    public class CInfinityCrys : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = 10;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<InfinityCrys>(), 1);
            recipe.AddIngredient(ModContent.ItemType<NeutronFragment>(), 1);
            recipe.AddTile(ModContent.TileType<CrystalCracker>());
            recipe.Register();
        }
    }
}
