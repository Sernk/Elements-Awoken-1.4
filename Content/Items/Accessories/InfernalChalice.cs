using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Accessories
{
    public class InfernalChalice : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<InfernalFury>()] < 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<InfernalFury>(), 20, 1f, Main.myPlayer, i, 0f);
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.SteampunkCup, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}