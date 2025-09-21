using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class EnergyStorm : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.sentry = true;
            Projectile.timeLeft = Projectile.SentryLifeTime + 300;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6) 
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 5)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 15, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Main.dust[num1].noGravity = true;
            Main.dust[num1].velocity *= 0.9f;
        
            if (Projectile.timeLeft <= 300f || (ProjectileUtils.CountProjectiles(Projectile.type,Projectile.owner) > 1 && ProjectileUtils.HasLeastTimeleft(Projectile.whoAmI)))
            {
                Projectile.alpha += 5;
                if (Projectile.alpha > 255)
                {
                    Projectile.alpha = 255;
                    Projectile.Kill();
                }
            }

            Projectile.localAI[1]--;
            if (Projectile.owner == Main.myPlayer)
            {
                float max = 400f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                        if (Projectile.localAI[1] <= 0)
                        {
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            //Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 12);
                            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<LightningBlast>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                            Projectile.localAI[1] = Main.rand.Next(16,60);
                        }
                    }
                }
            }
        }
    }
}