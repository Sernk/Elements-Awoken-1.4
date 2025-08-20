using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Volcanox
{
    public class VolcanoxExplosion : ModProjectile
    {
        public int dustCooldown = 0;
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
        }
        public override void AI()
        {
            int maxdusts = 20;
            if (dustCooldown <= 0)
            {
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = 100 + Main.rand.Next(30);
                    float dustSpeed = 10;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(Projectile.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                    vortex.noGravity = true;

                    dustCooldown = 5;
                }
            }
        }
    }
}