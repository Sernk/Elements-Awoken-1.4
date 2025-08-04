using ElementsAwoken.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CorruptedFangP : ModProjectile
    {
        private bool[] hasHit = new bool[Main.maxNPCs];
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].AddBuff(ModContent.BuffType<VilePower>(), 300, false);

            hasHit[target.whoAmI] = true;
            bool moreEnemies = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.whoAmI != target.whoAmI && Vector2.Distance(nPC.Center, Projectile.Center) < 600 && nPC.active && nPC.CanBeChasedBy(Projectile, false) && Collision.CanHitLine(Projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    moreEnemies = true;
                }
            }
            if (!moreEnemies) Projectile.Kill();
        }
        public override bool ShouldUpdatePosition()
        {
            if (Projectile.ai[1] < 45) return false;
            return base.ShouldUpdatePosition();
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.ai[1]++;

            if (Projectile.ai[1] >= 45)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Main.rand.NextBool(6) ? 75 : 46);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.8f;
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
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 46, 0f, 0f, 100, default);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}