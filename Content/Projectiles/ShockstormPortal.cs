using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ShockstormPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2)
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
            Player player = Main.player[Projectile.owner];
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] < 300f)
            {
                Projectile.alpha -= 5;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
            else
            {
                Projectile.alpha += 5;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
            int num3;
            if (Projectile.alpha < 150 && Projectile.ai[0] < 180f)
            {
                for (int num849 = 0; num849 < 1; num849 = num3 + 1)
                {
                    float num850 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num850 < -0.5f)
                    {
                        num850 = -0.5f;
                    }
                    if (num850 > 0.5f)
                    {
                        num850 = 0.5f;
                    }
                    Vector2 value37 = new Vector2((float)(-(float)Projectile.width) * 0.2f * Projectile.scale, 0f).RotatedBy((double)(num850 * 6.28318548f), default(Vector2)).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                    int num851 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, 226, -Projectile.velocity.X / 3f, -Projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num851].position = Projectile.Center + value37;
                    Main.dust[num851].velocity = Vector2.Normalize(Main.dust[num851].position - Projectile.Center) * 2f;
                    Main.dust[num851].noGravity = true;
                    num3 = num849;
                }
                for (int num852 = 0; num852 < 1; num852 = num3 + 1)
                {
                    float num853 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num853 < -0.5f)
                    {
                        num853 = -0.5f;
                    }
                    if (num853 > 0.5f)
                    {
                        num853 = 0.5f;
                    }
                    Vector2 value38 = new Vector2((float)(-(float)Projectile.width) * 0.6f * Projectile.scale, 0f).RotatedBy((double)(num853 * 6.28318548f), default(Vector2)).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                    int num854 = Dust.NewDust(Projectile.Center - Vector2.One * 5f, 10, 10, 226, -Projectile.velocity.X / 3f, -Projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num854].velocity = Vector2.Zero;
                    Main.dust[num854].position = Projectile.Center + value38;
                    Main.dust[num854].noGravity = true;
                    num3 = num852;
                }
            }

            Projectile.localAI[1]--;
            float max = 1000f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                {
                    if (Projectile.localAI[1] <= 0)
                    {
                        Vector2 vector94 = nPC.Center - Projectile.Center;
                        float ai = (float)Main.rand.Next(100);
                        Vector2 vector95 = Vector2.Normalize(vector94.RotatedByRandom(0.78539818525314331)) * 9f;
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, vector95.X, vector95.Y, ModContent.ProjectileType<LightningArc>(), (int)Projectile.ai[1], 0f, Main.myPlayer, vector94.ToRotation(), ai);
                        SoundEngine.PlaySound(SoundID.Item122, Projectile.position);

                        Projectile.localAI[1] = Main.rand.Next(15, 60);
                    }
                }
            }
        }   
    }
}