using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class ManashardDaggerP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 27;
            Projectile.timeLeft = 600;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 234);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.785f;

            Projectile.localAI[1]++;
            if (Projectile.localAI[1] > 50)
            {
                Projectile.velocity.Y += 0.16f;
            }
        }
    }
}