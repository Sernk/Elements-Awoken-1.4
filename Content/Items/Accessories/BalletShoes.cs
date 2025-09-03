using ElementsAwoken.EASystem.EARecipeSystem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class BalletShoes : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.rare = 3;
            Item.value = Item.sellPrice(0, 0, 2, 50);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 1.4f;
            if(player.controlJump && player.velocity.Y == 0 && player.velocity.X < player.direction * 15f)
            {
                player.velocity.X = player.direction * 15f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PinkThread, 15);
            recipe.AddRecipeGroup(EARecipeGroups.IronBar, 6);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}