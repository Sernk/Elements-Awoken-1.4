using ElementsAwoken.Content.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Thrown
{
    public class BalladOfBalance : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;  
            Item.height = 38;
            Item.damage = 85;
            Item.knockBack = 4f;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 7;
            Item.shoot = ModContent.ProjectileType<BalladOfBalanceP>();
            Item.shootSpeed = 16f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<BalladOfBalanceP>()] >= 4)
            {
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadeOfLight>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DanceOfDarkness>(), 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
