using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Putrid
{
    public class PutridBar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = 7;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PutridOre>(), 3);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}