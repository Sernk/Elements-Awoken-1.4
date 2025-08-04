using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PandemoniumFlame : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.alpha = 0;
            Projectile.scale = 0.6f;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300; 
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            int num = ModContent.GetInstance<Config>().lowDust ? 9 : 4;

            Projectile.ai[0]++;
            if (Projectile.ai[0] % num == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y) - Projectile.velocity * 0.5f, Projectile.width - 8, Projectile.height - 8, Main.rand.Next(4) == 0 ? 31: 127, 0f, 0f, 100, default(Color), 0.5f)];
                dust.fadeIn = 1f + Main.rand.NextFloat(-0.5f,0.5f);
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            ProjectileUtils.Home(Projectile, 6f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle rect = new Rectangle(0, 4, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height - 4);
                if (k == 0) rect.Y -= 4;
                float scale = 1 - ((float)k / (float)Projectile.oldPos.Length);
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color, Projectile.rotation, drawOrigin, scale * Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}