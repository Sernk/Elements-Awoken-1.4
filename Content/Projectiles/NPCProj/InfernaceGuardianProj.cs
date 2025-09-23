using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class InfernaceGuardianProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 86;
            Projectile.height = 86;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.alpha > 150) return false;
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            Projectile.rotation += 0.05f;
            if (Projectile.ai[0] == 0)
            {
                Projectile.scale = 0.001f;
                Projectile.ai[0]++;
            }
            if (Projectile.alpha > 100) Projectile.alpha -= 255 / 90;
            else Projectile.alpha = 100;
            if (Projectile.scale < 1) Projectile.scale += 1f / 60f;
            else Projectile.scale = 1;

            int dust = Dust.NewDust(Projectile.Center - new Vector2(Projectile.width / 2, Projectile.height / 2) * Projectile.scale, (int)(Projectile.width * Projectile.scale), (int)(Projectile.height * Projectile.scale), DustID.Torch);
            Main.dust[dust].velocity = -Projectile.velocity;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
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
            ProjectileUtils.HostileExplosion(Projectile, new int[] { 6 }, Projectile.damage);
        }
    }
}