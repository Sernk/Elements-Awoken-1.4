using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class StellarMeteor : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 180, false);
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.scale = Projectile.ai[1];
            Projectile.rotation += Projectile.velocity.X * 2f;
            Vector2 position = Projectile.Center + Vector2.Normalize(Projectile.velocity) * 10f;
            Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, 0f, 0f, 0, default(Color), 1f)];
            dust.position = position;
            dust.velocity = Projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * 0.33f + Projectile.velocity / 4f;
            dust.position += Projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2));
            dust.fadeIn = 0.5f;
            dust.noGravity = true;
            Dust dust1 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, 0f, 0f, 0, default(Color), 1f)];
            dust1.position = position;
            dust1.velocity = Projectile.velocity.RotatedBy(-1.5707963705062866, default(Vector2)) * 0.33f + Projectile.velocity / 4f;
            dust1.position += Projectile.velocity.RotatedBy(-1.5707963705062866, default(Vector2));
            dust1.fadeIn = 0.5f;
            dust1.noGravity = true;
            for (int num187 = 0; num187 < 1; num187++)
            {
                int num188 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 180, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num188].velocity *= 0.5f;
                Main.dust[num188].scale *= 1.3f;
                Main.dust[num188].fadeIn = 1f;
                Main.dust[num188].noGravity = true;
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
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 180, damageType: "magic");
        }
    }
}