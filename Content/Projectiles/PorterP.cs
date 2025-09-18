using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PorterP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            bool immune = false;
            foreach (int k in ElementsAwoken.instakillImmune)
            {
                if (target.type == k)
                {
                    immune = true;
                }
            }
            if (!immune && target.active && target.damage > 0 && !target.dontTakeDamage && !target.boss && target.lifeMax < 1000)
            {
                Vector2 newPos = new Vector2(target.Center.X, target.Center.Y);
                bool isntColliding = false;
                int num = 0;
                while (!isntColliding && num < 300)
                {
                    num++;
                    newPos = new Vector2(target.Center.X + Main.rand.Next(-300, 300), target.Center.Y + Main.rand.Next(-300, 300));
                    Point newPoint = newPos.ToTileCoordinates();
                    bool colliding = Main.tile[newPoint.X, newPoint.Y].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[newPoint.X, newPoint.Y].TileType] && !Main.tileSolidTop[(int)Main.tile[newPoint.X, newPoint.Y].TileType] && Main.tile[newPoint.X, newPoint.Y].TileType != TileID.Rope;
                    if (!colliding)
                    {
                        isntColliding = true;
                    }
                }
                ProjectileUtils.OutwardsCircleDust(Projectile, EAU.PinkFlame, 36, 3f, targetX: target.Center.X, targetY: target.Center.Y);
                target.Center = newPos;
                ProjectileUtils.OutwardsCircleDust(Projectile, EAU.PinkFlame, 36, 3f, targetX: target.Center.X, targetY: target.Center.Y);
            }
        }
    }
}