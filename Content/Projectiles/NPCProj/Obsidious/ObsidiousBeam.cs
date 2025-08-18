using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
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
            int type = 6;
            switch ((int)Projectile.ai[1])
            {
                case 0:
                    type = 6;
                    break;
                case 1:
                    type = 75;
                    break;
                case 2:
                    type = 135;
                    break;
                case 3:
                    type = EAU.PinkFlame;
                    break;
                default: break;
            }
            if (Projectile.localAI[0] > 9f)
            {
                int dustlength = 1;
                for (int i = 0; i < dustlength; i++)
                {
                    Vector2 vector33 = Projectile.position;
                    vector33 -= Projectile.velocity * ((float)i * (1 / dustlength));
                    Projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, 1, 1, type, 0f, 0f, 0, default(Color), 0.75f);
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
            int type = 6;
            switch ((int)Projectile.ai[1])
            {
                case 0:
                    type = 6;
                    break;
                case 1:
                    type = 75;
                    break;
                case 2:
                    type = 135;
                    break;
                case 3:
                    type = EAU.PinkFlame;
                    break;
                default: break;
            }
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, type, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}