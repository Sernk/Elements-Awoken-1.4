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
    public class MeteoricBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Projectile.scale *= 0.8f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Meteoric Bolt");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 120, false);
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0]++;
                Projectile.ai[1] = Main.rand.Next(40, 90);
            }
            Projectile.ai[1]--;
            if (Projectile.ai[1] <= 0) Projectile.Kill();

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Main.rand.NextBool(3))
                {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.5f;
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
            ProjectileUtils.Explosion(Projectile, new int[] { 6 }, Projectile.damage,"magic");
        }
    }
}