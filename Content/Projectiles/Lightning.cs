using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Lightning : ModProjectile
    {        
        public Vector2 velocity = new();
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 200;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(2) == 0)
            {
                Projectile.ai[0]++;
                Projectile.ai[1] = target.whoAmI;
            }
            else Projectile.Kill();
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

            int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.color = new Color(154, 255, 145);
            }
            if (velocity == Vector2.Zero) velocity = Projectile.velocity;
            Projectile.velocity = velocity.RotatedByRandom(.75);

            if (Projectile.ai[0] != 0)
            {
                float closestEntity = 200f;
                NPC target = null;
                for (int i = 0; i < 200; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, nPC.Center, 1, 1))
                    {
                        float dist = Math.Abs(Projectile.Center.X - nPC.Center.X) + Math.Abs(Projectile.Center.Y - nPC.Center.Y);
                        if (dist < closestEntity && nPC.whoAmI != Projectile.ai[1])
                        {
                            closestEntity = dist;
                            target = nPC;
                        }
                    }
                }
                if (target != null)
                {
                    Vector2 toTarget = target.Center - Projectile.Center;
                    toTarget.Normalize();
                    toTarget *= 5;
                    velocity = toTarget;
                }
            }
        }
    }
}