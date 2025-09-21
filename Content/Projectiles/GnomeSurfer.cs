using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class GnomeSurfer : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 0;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 18)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.spriteDirection = Main.rand.Next(2) == 0 ? -1 : 1;
                Projectile.localAI[0]++;
            }

            Projectile.ai[0]++;
            float max = 500f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                {
                    float Speed = 9f;
                    float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                    if (Projectile.ai[0] == 15)
                    {
                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<GnomeBolt>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                }
            }
            if (Projectile.ai[0] >= 30)
            {
                Projectile.alpha += 30;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            int numDusts = 20;
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = (Vector2.Normalize(new Vector2(5,5)) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                Vector2 velocity = position - Projectile.Center;
                int dust = Dust.NewDust(position + velocity, 0, 0, 134, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 1.5f;
            }
        }
    }
}