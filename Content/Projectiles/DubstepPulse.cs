using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DubstepPulse : ModProjectile
    {       
        public int dustType = 60;

        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                dustType = Main.rand.Next(219, 224);
                Projectile.localAI[0]++;
            }
            Projectile.localAI[1]++;
            if (Projectile.localAI[1] % 5 == 0)
            {
                int numDusts = 20;
                for (int i = 0; i < numDusts; i++)
                {
                    Vector2 position = (Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                    Vector2 velocity = position - Projectile.Center;
                    int dust = Dust.NewDust(position + velocity, 0, 0, dustType, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
                }
            }
        }
    }
}