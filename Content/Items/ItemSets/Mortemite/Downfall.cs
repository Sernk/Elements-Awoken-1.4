using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Mortemite
{
    public class Downfall : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 110;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 16;
            Item.height = 14;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            //innacurate fire
            Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(7));
            speed.X = perturbedSpeed.X;
            speed.Y = perturbedSpeed.Y;
            if (Main.rand.Next(5) == 0)
            {
                SoundEngine.PlaySound(SoundID.Item72, player.position);
                Projectile.NewProjectile(source, position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<MortemiteBlade>(), Item.damage * 2, 0, player.whoAmI, 0f, 0f);
                return false;
            }
            return true;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .50f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MortemiteDust>(), 50);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}