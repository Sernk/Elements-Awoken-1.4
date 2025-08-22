using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.Buffs.Debuffs;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class ShadeWyrmBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.2f, 0.25f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (!ModContent.GetInstance<Config>().lowDust && Main.rand.NextBool(2))
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<HandsOfDespair>(), 80, true);
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