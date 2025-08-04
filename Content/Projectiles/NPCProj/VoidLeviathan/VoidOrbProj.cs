using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidOrbProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
            Projectile.scale = 0.6f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.2f, 0.5f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (!ModContent.GetInstance<Config>().lowDust && Main.rand.Next(2) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Pink)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            Player P = Main.player[Main.myPlayer];
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 60)
            {
                float speed = 3f;
                float toX = P.Center.X - Projectile.Center.X;
                float toY = P.Center.Y - Projectile.Center.Y;
                float num6 = (float)Math.Sqrt((double)(toX * toX + toY * toY));
                num6 = speed / num6;
                toX *= num6;
                toY *= num6;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + toX) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + toY) / 21f;
            }
            if (Projectile.ai[0] > 180)
            {
                Projectile.alpha += 255 / 30;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]);
                EAU.Sb.Draw(tex, drawPos, rectangle, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}