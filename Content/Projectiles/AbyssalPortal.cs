using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AbyssalPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            Projectile.light = 1f;
            Projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Portal");
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
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Pink, 0f, 0f, 100, default(Color)); // DustID.PinkFlame
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 3;
            }
        }
        public override void AI()
        {
            Projectile.velocity *= 0.99f;
            Projectile.localAI[0]++;
            if (Projectile.ai[1] != 0 || Projectile.ai[0] > 300)
            {
                Projectile.alpha += 255 / 30;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
            else if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 255 / 30;
            }
            if (Main.rand.NextBool(6))
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Firework_Pink, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
            Projectile.ai[0]++;
            if (Projectile.ai[1] == 0 && Main.rand.NextBool(20) && Projectile.owner == Main.myPlayer && Projectile.alpha <= 0)
            {
                float max = 600f;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                        Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, projSpeed.X, projSpeed.Y, ModContent.ProjectileType<AbyssalBall>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                        Projectile.ai[1] = 1;
                        break;
                    }
                }
            }
        }
    }
}