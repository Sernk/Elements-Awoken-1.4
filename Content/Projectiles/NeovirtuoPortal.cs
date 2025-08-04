using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class NeovirtuoPortal : ModProjectile
    {
        public int laserTimer = 5;
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1.3f;
            Projectile.timeLeft = 600;
            Main.projFrames[Projectile.type] = 4;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Projectile.velocity.X *= 0.9f;
            Projectile.velocity.X *= 0.9f;
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            laserTimer--;
            float max = 400f;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max && nPC.damage > 0)
                {
                    Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
                    int type = ModContent.ProjectileType<NeovirtuoBolt>();
                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(vector8.Y - (nPC.position.Y + (nPC.height * 0.5f)), vector8.X - (nPC.position.X + (nPC.width * 0.5f)));
                    if (laserTimer <= 0)
                    {
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                        Projectile.NewProjectile(EAU.Proj(Projectile), vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 60, 0f, Main.myPlayer, 0f, 0f);
                        laserTimer = 5;
                    }
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}