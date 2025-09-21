using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Weapons.Ranged
{
    public class StormStrike : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 26;
            Item.damage = 190;
            Item.knockBack = 3.5f;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item61;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.rare = 10;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.shootSpeed = 10f;
            Item.shoot = 10;
            Item.useAmmo = AmmoID.Rocket;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Strike");
            // Tooltip.SetDefault("Fires a storm of homing rockets\nHas a chance to fire a Mega Rocket\n70% chance to not consume ammo");
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 5;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(8)); 
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<HomingRocket>(), damage, knockback, player.whoAmI);
            }
            if (Main.rand.Next(2) == 0)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(0));
                int num1 = damage * 2;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Projectiles.MegaRocket>(), num1, knockback, player.whoAmI);
            }
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.Next(0, 100) <= 70)
                return false;
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MegaRocket>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
