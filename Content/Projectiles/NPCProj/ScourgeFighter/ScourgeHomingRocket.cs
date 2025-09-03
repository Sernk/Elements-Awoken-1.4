using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.ScourgeFighter
{
    public class ScourgeHomingRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 150;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] == 0f)
            {
                //Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
                Projectile.localAI[0] = 1f;
            }
            if (Projectile.localAI[0] >= 1 && Projectile.localAI[0] < 200)
            {
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - Projectile.position.Y, Main.player[Main.myPlayer].position.X - Projectile.position.X);
                Projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 7;
            }

            for (int num255 = 0; num255 < 2; num255++)
            {
                float num256 = 0f;
                float num257 = 0f;
                if (num255 == 1)
                {
                    num256 = Projectile.velocity.X * 0.5f;
                    num257 = Projectile.velocity.Y * 0.5f;
                }
                Vector2 position71 = new Vector2(Projectile.position.X + 3f + num256, Projectile.position.Y + 3f + num257) - Projectile.velocity * 0.5f;
                int width67 = Projectile.width - 8;
                int height67 = Projectile.height - 8;
                int num258 = Dust.NewDust(position71, width67, height67, EAU.PinkFlame, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[num258];
                dust.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                dust = Main.dust[num258];
                dust.velocity *= 0.2f;
                Main.dust[num258].noGravity = true;
                Vector2 position72 = new Vector2(Projectile.position.X + 3f + num256, Projectile.position.Y + 3f + num257) - Projectile.velocity * 0.5f;
                int width68 = Projectile.width - 8;
                int height68 = Projectile.height - 8;
                num258 = Dust.NewDust(position72, width68, height68, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num258].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                dust = Main.dust[num258];
                dust.velocity *= 0.05f;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.HostileExplosion(Projectile, EAU.PinkFlame);
        }
    }
}
 