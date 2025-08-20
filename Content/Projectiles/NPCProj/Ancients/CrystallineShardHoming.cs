using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients
{
    public class CrystallineShardHoming : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 120;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Amalgamate");
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Player player = Main.player[Main.myPlayer];
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] >= 30)
            {
                float toX = player.Center.X - Projectile.Center.X;
                float toY = player.Center.Y - Projectile.Center.Y;
                float num6 = (float)Math.Sqrt((double)(toX * toX + toY * toY));
                num6 = 9f / num6;
                toX *= num6;
                toY *= num6;

                Projectile.velocity = new Vector2((Projectile.velocity.X * 20f + toX) / 21f, (Projectile.velocity.Y * 20f + toY) / 21f);
            }
            if (Projectile.Hitbox.Intersects(new Rectangle((int)player.Center.X - 4, (int)player.Center.Y - 4, 8, 8)))
            {
                Projectile.Kill();
            }
            if (Main.rand.Next(3) == 0 && !ModContent.GetInstance<Config>().lowDust)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.GetDustID());
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}