using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CoilRound : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.3f, 0.2f, 0.6f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.NextBool(4))
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229)];
                dust.noGravity = true;
                dust.scale = 1f;
                dust.velocity *= 0.1f;
                dust.color = new Color(0,0,255);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/CoilTrail").Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                if (k == 0) continue;
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width / 2, Projectile.height / 2) - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)Projectile.oldPos.Length);
                EAU.Sb.Draw(tex, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 229, Projectile.oldVelocity.X * 0.25f, Projectile.oldVelocity.Y * 0.25f)];
                dust.color = new Color(0, 0, 255);
            }
        }
    }
}