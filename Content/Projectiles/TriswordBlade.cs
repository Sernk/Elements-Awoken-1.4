using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class TriswordBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.alpha != 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0]++;
            Vector2 slashPos =  new Vector2(1, 1);
            switch (Main.rand.Next(4))
            {
                case 0:
                    slashPos =new Vector2(1, 1);
                    break;
                case 1:
                    slashPos = new Vector2(1, -1);
                    break;
                case 2:
                    slashPos = new Vector2(-1, -1);
                    break;
                case 3:
                    slashPos =  new Vector2(-1, 1);
                    break;
                default:
                    break;
            }
            slashPos = target.Center + slashPos * 50;
            Vector2 slashVel = target.Center - slashPos;
            slashVel.Normalize();
            slashVel *= 5f;
            Projectile.NewProjectile(EAU.Proj(Projectile), slashPos.X, slashPos.Y, slashVel.X, slashVel.Y, ModContent.ProjectileType<TriswordSlash>(), Projectile.damage / 2, 0f, Projectile.owner, 0f, 0f);
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
            Projectile.ai[1]++;
            if (Projectile.ai[0] != 0 || Projectile.ai[1] > 240) Projectile.alpha += 255 / 30;
            if (Projectile.alpha >= 255) Projectile.Kill();
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