using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Other
{
    public class GlassHeart : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.glassHeart = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 16);
            recipe.AddIngredient(ItemID.LifeCrystal, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}