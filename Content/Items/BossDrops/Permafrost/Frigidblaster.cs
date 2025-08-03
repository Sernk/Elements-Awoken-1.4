using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Permafrost
{
    public class Frigidblaster : ModItem
    {
        public override void SetDefaults()
        {
            Item.knockBack = 4;
            Item.damage = 48;
            Item.width = 38;
            Item.height = 18;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 46, 0, 0);
            Item.rare = 7;
            Item.shoot = 10;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item36;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Frigidbullet>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}