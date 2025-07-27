using ElementsAwoken.EASystem;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Materials
{
    public class Capacitor : ModItem
    {
        // T3

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 2;
        }
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Capacitor");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(EARecipeGroups.CopperBar, 4);
            recipe.AddIngredient(ModContent.ItemType<CopperWire>(), 5);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}