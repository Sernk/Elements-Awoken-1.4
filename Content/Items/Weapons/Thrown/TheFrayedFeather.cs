using ElementsAwoken.Content.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Thrown
{
    public class TheFrayedFeather : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.damage = 18;
            Item.knockBack = 3f;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.useTime = 5;
            Item.useAnimation = 15;
            Item.reuseDelay = 15;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item39;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = 2;
            Item.shoot = ModContent.ProjectileType<FrayedFeather>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float rotation = MathHelper.ToRadians(1f);
            float amount = player.direction == -1 ? player.itemAnimation - 15 / 2: -player.itemAnimation + 15 / 2; // change 15 to use animation time
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, amount));
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FrayedFeather>(), damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ThrowingKnife, 150);
            recipe.AddIngredient(ItemID.Feather, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}