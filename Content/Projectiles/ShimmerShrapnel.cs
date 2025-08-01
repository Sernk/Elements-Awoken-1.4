using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ShimmerShrapnel : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[Projectile.type]) * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

                Rectangle rectangle = new Rectangle(0, 0, tex.Width, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame);
                Const.Sb.Draw(tex, drawPos, rectangle, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            Projectile.frame = (int)Projectile.ai[0];
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 3;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.ai[0] = Main.rand.Next(0, 3);
                Projectile.localAI[0] = 1;
            }
            Projectile.ai[1]++;
            if (Projectile.ai[1] > 60)
            {
                Projectile.alpha += 10;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(4) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, GetDustID());
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }
        private int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return ModContent.DustType<AncientRed>();
                case 1:
                    return ModContent.DustType<AncientGreen>();
                case 2:
                    return ModContent.DustType<AncientBlue>();
                case 3:
                    return ModContent.DustType<AncientPink>();
                default:
                    return ModContent.DustType<AncientRed>();
            }
        }
    }
}