using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class Shotstorm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 26;
            Item.damage = 190;
            Item.knockBack = 6f;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item17;
            Item.shootSpeed = 24f;
            Item.shoot = ProjectileType<ShotstormDart>();
            Item.useAmmo = AmmoID.Dart;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(1.5f);
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 60f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<ShotstormDart>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DartPistol, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 6);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DartRifle, 1);
            recipe.AddIngredient(ItemID.FragmentVortex, 6);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
