using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class VoidPortal : ModProjectile
    {
        public float shootTimer = 0f;
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            Projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, EAU.PinkFlame, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            if(Projectile.localAI[0] >= 600)
            {
                Projectile.alpha += 5;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }

            int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, EAU.PinkFlame, 0f, 0f, 100, default(Color));
            Main.dust[dust].noGravity = true;
            float speed = 0.6f;
            if (Vector2.Distance(Projectile.Center, Main.MouseWorld) >= 500)
            {
                speed = 1f;
            }

            Vector2 toTarget = new Vector2(Main.MouseWorld.X - Projectile.Center.X, Main.MouseWorld.Y - Projectile.Center.Y);
            toTarget.Normalize();
            Projectile.velocity += toTarget * speed;

            shootTimer--;
            float max = 600f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                    if (shootTimer <= 0)
                    {
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        SoundEngine.PlaySound(SoundID.Item103, Projectile.position);
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, projSpeed.X, projSpeed.Y, ModContent.ProjectileType<VoidPortalSinewave>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        shootTimer = 5;
                    }
                }
            }
        }
    }
}