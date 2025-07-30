using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Ancient.Xernon
{
    public class LamentIII : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.damage = 96;
            Item.mana = 15;
            Item.knockBack = 6;
            Item.crit = 12;
            Item.useStyle = 5;
            Item.useTime = 7;
            Item.useAnimation = 21;
            Item.staff[Item.type] = true;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<LamentBallExplosive>();
            Item.shootSpeed = 13f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(7));
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            if (Main.rand.Next(5) == 0)
            {
                SoundEngine.PlaySound(SoundID.Item88, player.position);
                int choice = Main.rand.Next(2);
                if (choice == 0)
                {
                    Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<LamentWave>(), (int)(Item.damage * 1.5f), knockback, player.whoAmI, 0f, 0f);
                }
                else if (choice == 1)
                {
                    Projectile.NewProjectile(source, position.X, position.Y, speed.X * 1.3f, speed.Y * 1.3f, ModContent.ProjectileType<LamentPierce>(), (int)(Item.damage * 1.2f), knockback, player.whoAmI, 0f, 0f);
                }
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LamentII>(), 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

        }
    }
}
