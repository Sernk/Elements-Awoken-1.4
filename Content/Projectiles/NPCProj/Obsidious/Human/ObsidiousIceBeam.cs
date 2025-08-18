using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousIceBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            if (Projectile.velocity.X != Projectile.velocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = -Projectile.velocity.X;
            }
            if (Projectile.velocity.Y != Projectile.velocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = -Projectile.velocity.Y;
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 9f)
            {
                int dustlength = 2;
                for (int i = 0; i < dustlength; i++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * ((float)i * (1 / dustlength));
                    Projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, 1, 1, 135, 0f, 0f, 0, default(Color), 0.75f);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.05f;
                    Main.dust[num448].noGravity = true;
                }
                return;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 135, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}