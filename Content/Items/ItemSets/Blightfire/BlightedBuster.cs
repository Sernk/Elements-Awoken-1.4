using ElementsAwoken.Content.Projectiles.Bullets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Blightfire
{
    public class BlightedBuster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 18;
            Item.damage = 140;
            Item.knockBack = 15;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 11;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item36;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int angle = 3;
            if (player.altFunctionUse == 2) angle = 20;
            if (type == ProjectileID.Bullet) type = ModContent.ProjectileType<BlightedBulletP>();
            int numberProjectiles = 4 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(angle));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            Vector2 toTarget = new Vector2(Main.MouseWorld.X - player.Center.X, Main.MouseWorld.Y - player.Center.Y);
            toTarget.Normalize();
            bool tooQuick = false;
            if (toTarget.X < 0 && player.velocity.X > 8f) tooQuick = true;
            if (toTarget.X > 0 && player.velocity.X < -8f) tooQuick = true;
            if (!tooQuick) player.velocity -= toTarget * 3f;
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Blightfire>(), 10);
            recipe.AddIngredient(ItemID.LunarBar, 2);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}