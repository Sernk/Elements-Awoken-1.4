using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions.MinionProj
{
    public class PutridVomit : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = 3;
            Projectile.friendly = true;
            Projectile.alpha = 100;
            Projectile.timeLeft = 180;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Putrid Ripper");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 600, false);
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.16f;

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(4) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.GetDustID());
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
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
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.GetDustID(), Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default(Color), 1.8f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
    }
}