using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Materials
{
    public class GoldWire : ModItem
    {
        // T2
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddRecipeGroup(EARecipeGroups.GoldBar, 4);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 5);
            recipe.AddIngredient(ModContent.ItemType<LensFragment>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}