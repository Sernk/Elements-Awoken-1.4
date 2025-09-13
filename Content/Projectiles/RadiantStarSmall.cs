using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class RadiantStarSmall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/RadiantStar"; } }
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.scale = 0.6f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                float alpha = (1 - Projectile.alpha / 255) - ((float)k / (float)Projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)Projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(127, 3, 252), (float)k / (float)Projectile.oldPos.Length) * alpha;
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Starstruck>(), 300);

            target.immune[Projectile.owner] = 5;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] < 15 || target.whoAmI != Projectile.ai[1]) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            Projectile.rotation += 0.05f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] == 15f)  Projectile.velocity = -Projectile.velocity;
            else if (Projectile.ai[0] > 30f) Projectile.Kill();
        }
        public override void OnKill(int timeLeft)
        {
            int numDust = ModContent.GetInstance<Config>().lowDust ? 5 : 16;
            ProjectileUtils.OutwardsCircleDust(Projectile, EAU.PinkFlame, numDust, 6f, randomiseVel: true, dustScale: 2f);
        }
    }
}