using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheCelestial
{
    public class Celestia : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 28;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 50;
            Item.knockBack = 4;
            Item.useTime = 46;
            Item.useAnimation = 46;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item12;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<CelestiaPortal>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(1, 3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CelestiaPortal>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}