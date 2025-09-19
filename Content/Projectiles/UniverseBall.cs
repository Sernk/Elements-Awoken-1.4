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
    public class UniverseBall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Universe Ball");
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            ProjectileUtils.Home(Projectile, 5f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            Texture2D outlineTex = ModContent.Request<Texture2D>(EAU.ModifyProjTexture("UniverseBall1")).Value;
            int trailNum = 0;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                for (int i = 0; i < 2; i++) // to draw between the positions
                {
                    trailNum++;
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    if (k < Projectile.oldPos.Length - 1) drawPos = (Projectile.oldPos[k] + Projectile.oldPos[k + i]) / 2 - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    float alpha = 1 - ((float)trailNum / (float)(Projectile.oldPos.Length * 2));
                    Color color = Color.Lerp(Color.White, new Color(85, 44, 156), (float)trailNum / (float)(Projectile.oldPos.Length*2)) * alpha;
                    float scale = 1 - ((float)trailNum / (float)(Projectile.oldPos.Length * 2));
                    EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
    }
}