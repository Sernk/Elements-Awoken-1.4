using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class ExtinctionArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.arrow = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Lighting.AddLight((int)Projectile.Center.X, (int)Projectile.Center.Y, 0.6f, 0.1f, 0.5f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 5f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 200);
            target.immune[Projectile.owner] = 9;
        }
    }
}