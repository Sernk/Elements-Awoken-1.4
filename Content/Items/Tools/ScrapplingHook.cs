using ElementsAwoken.Content.Projectiles.Hooks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Tools
{
    public class ScrapplingHook : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.rare = 0;
            Item.shootSpeed = 5.5f;
            Item.shoot = ModContent.ProjectileType<ScrapplingHookP>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}