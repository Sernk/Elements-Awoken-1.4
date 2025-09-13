using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles
{
    public class RadiantStarThrown : ModProjectile
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
        public override void AI()
        {
            Projectile.rotation += 0.05f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 15f) ProjectileUtils.Home(Projectile, 8f, 800f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffType<Buffs.Debuffs.Starstruck>(), 300);

            float rotation = MathHelper.TwoPi;
            float numProj = Main.rand.Next(5, 7);
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = (rotation / numProj * i).ToRotationVector2() * 4.5f;
                Projectile.NewProjectile(EAU.Proj(Projectile), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<RadiantStarSmall>(), (int)(Projectile.damage * 0.6f), Projectile.knockBack, Projectile.owner, 0,target.whoAmI);
            }
        }
    }
}