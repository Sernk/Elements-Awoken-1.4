using ElementsAwoken.Content.Projectiles.Explosions;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidRunes : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 20;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = (int)Projectile.ai[0];
            return true;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.2f, 0.55f);

            if (Projectile.localAI[0] == 0)
            {
                Projectile.ai[0] = Main.rand.Next(20);
                Projectile.localAI[0]++;
            }

            Projectile.ai[1]++;
            int timeAlive = Main.expertMode ? 160 : 200;
            if (MyWorld.awakenedMode) timeAlive = 120;
            if (Projectile.ai[1] < 60)
            {
                Projectile.alpha -= 255 / 60;
            }
            if (Projectile.ai[1] > timeAlive)
            {
                Projectile.Kill();
            }

            if (!ModContent.GetInstance<Config>().lowDust)
            {
                int maxDist = (int)(Projectile.width * 1.2f);
                int numDust = (int)MathHelper.Lerp(15, 0, (float)Projectile.alpha / 255f);
                for (int i = 0; i < numDust; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                    Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset - Vector2.One * 4, 0, 0, Const.PinkFlame, 0, 0, 100)];
                    dust.noGravity = true;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                ProjectileUtils.HostileExplosion(Projectile, new int[] { Const.PinkFlame }, Projectile.damage);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<VoidRuneExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
        }
        public override void PostDraw(Color lightColor)
        {
            if (ModContent.GetInstance<Config>().lowDust)
            {
                Texture2D auraTex = ModContent.Request<Texture2D>("Projectiles/Content/NPCProj/VoidLeviathan/VoidRunesCircle").Value;
                Vector2 drawOrigin = new Vector2(auraTex.Width * 0.5f, auraTex.Height * 0.5f);
                Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Const.Sb.Draw(auraTex, drawPos, null, Color.White * (1-((float)Projectile.alpha / 255f)), 0f, drawOrigin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}