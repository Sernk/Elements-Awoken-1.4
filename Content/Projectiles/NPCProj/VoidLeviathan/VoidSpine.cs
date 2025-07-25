using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.VoidLeviathan
{
    public class VoidSpine : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Spine");
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.alpha != 0) return false;
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.2f, 0.55f);

            Projectile.ai[0]++;
            if (Projectile.ai[1] == 0)
            {
                Projectile.scale = 0.05f;
                Projectile.ai[1]++;
            }
            if (Projectile.ai[0] < 15)
            {
                if (Projectile.scale < 0.5f) Projectile.scale += 0.5f / 15f;
                else Projectile.scale = 0.5f;
            }
            if (Projectile.ai[0] > 30)
            {
                Projectile.alpha += 255 / 30;
                if (Projectile.alpha >= 255)Projectile.Kill();
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (!ModContent.GetInstance<Config>().lowDust && Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Pink);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 80, true);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}