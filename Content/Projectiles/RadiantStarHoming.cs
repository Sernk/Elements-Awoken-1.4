using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class RadiantStarHoming : ModProjectile
    {
        int numCanHit = 6;
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/RadiantStar"; } }
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[1] < numCanHit)
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
            }
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] >= numCanHit) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Starstruck>(), 300);
            target.immune[Projectile.owner] = 5;
            Projectile.ai[1]++;
        }
        public override void AI()
        {
            if (Projectile.ai[1] < numCanHit)
            {
                if (Projectile.timeLeft <= 60 || (CountProjectiles() > 9 && HasLeastTimeleft()))
                {
                    Projectile.ai[1] = numCanHit;
                }
                Projectile.rotation += 0.05f;
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] >= 15f)
                {
                    ProjectileUtils.Home(Projectile, 8f, 800f);
                }
                Projectile.velocity *= 0.98f;
                ProjectileUtils.PushOtherEntities(Projectile);
            }
            else
            {
                Projectile.velocity *= 0.9f;
                ProjectileUtils.FadeOut(Projectile, 255 / 60);
                Vector2 oldCenter = Projectile.Center;
                Projectile.scale *= 0.98f;
                Projectile.Center = oldCenter;
            }
        }
        private bool HasLeastTimeleft()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Projectile.type && Projectile.timeLeft > Main.projectile[i].timeLeft && Main.projectile[i].ai[1] < numCanHit) return false;
            }
            return true;
        }
        private int CountProjectiles()
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Projectile.type && Main.projectile[i].owner == Projectile.owner && Main.projectile[i].ai[1] < numCanHit) num++;
            }
            return num;
        }
    }
}