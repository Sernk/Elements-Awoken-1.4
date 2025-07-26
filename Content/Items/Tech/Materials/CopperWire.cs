using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tech.Materials
{
    public class CopperWire : ModItem
    {
        // T1
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 0;
        }
        //public override void SetStaticDefaults()
        //{
        //    DisplayName.SetDefault("Copper Wire");
        //    Tooltip.SetDefault("Used for tech crafting");
        //}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddRecipeGroup(EARecipeGroups.CopperBar, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
