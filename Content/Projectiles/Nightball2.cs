using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class Nightball2 : ModProjectile
    {
        public override string Texture { get { return EAU.ModifyProjTexture("LaserTex"); } }
        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Lighting.AddLight((int)Projectile.Center.X, (int)Projectile.Center.Y, 0f, 0.1f, 0.5f);
            float trailLength = 20f;
            if (Projectile.ai[1] == 0f)
            {
                Projectile.localAI[0] += 3f;
                if (Projectile.localAI[0] > trailLength)
                {
                    Projectile.localAI[0] = trailLength;
                }
            }
            else
            {
                Projectile.localAI[0] -= 3f;
                if (Projectile.localAI[0] <= 0f)
                {
                    Projectile.Kill();
                    return;
                }
            }
            ProjectileUtils.Home(Projectile, 6f, 600);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(211, 28, 214, 0);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color color32 = Projectile.GetAlpha(lightColor);
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            int trailNum = 0;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                for (int i = 0; i < 2; i++) // to draw between the positions
                {
                    trailNum++;
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    if (k < Projectile.oldPos.Length - 1) drawPos = (Projectile.oldPos[k] + Projectile.oldPos[k + i]) / 2 - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    float alpha = 1 - ((float)trailNum / (float)(Projectile.oldPos.Length * 2));
                    float scale = 1 - ((float)trailNum / (float)(Projectile.oldPos.Length * 2));
                    EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color32, Projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
                }
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 173, damageType: "melee");
        }
    }
}