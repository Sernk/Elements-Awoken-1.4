using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class DesolationShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.localAI[0] <= 15)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            Projectile.rotation += 0.1f;
            Projectile.velocity *= 0.97f;
            if (Math.Abs(Projectile.velocity.X) < 0.1f && Math.Abs(Projectile.velocity.Y) < 0.1f)
            {
                Projectile.Kill();
            }
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AncientGreen>(), 0f, 0f, 100, default(Color), 1f)];
                dust.noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<AncientGreen>(), 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }
    }
}