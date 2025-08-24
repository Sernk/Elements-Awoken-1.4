using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class TimecleaverRound : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.2f, 0.4f, 0.9f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Color.Lerp(Color.White, new Color(119, 255, 0), (float)k / (float)Projectile.oldPos.Length) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            Vector2 drawPos2 = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos2, null, Color.White, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}