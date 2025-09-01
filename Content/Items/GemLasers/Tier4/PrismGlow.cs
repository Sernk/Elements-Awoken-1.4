using ElementsAwoken.Content.Items.GemLasers.Tier3;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Projectiles.GemLasers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.GemLasers.Tier4
{
    public class PrismGlow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 28;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.damage = 85;
            Item.knockBack = 4;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = 5;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 10;
            Item.shoot = ModContent.ProjectileType<GemLaserHoming>();
            Item.shootSpeed = 28f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int ai = 0;
            if (type == ProjectileID.Bullet)
            {
                type = ModContent.ProjectileType<GemLaserHoming>();
                SoundEngine.PlaySound(SoundID.Item12, player.position);
                ai = Main.rand.Next(7);
            }
            else SoundEngine.PlaySound(SoundID.Item11, player.position);
            int numberProjectiles = 1 + Main.rand.Next(2); 
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(7));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI,ai);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ModContent.ItemType<AmberBlaster>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AmethystBlaster>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiamondBlaster>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EmeraldBlaster>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RubyBlaster>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SapphireBlaster>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TopazBlaster>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 50);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}