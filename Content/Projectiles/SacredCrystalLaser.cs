using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SacredCrystalLaser : ModProjectile
    {	
        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 4;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Lighting.AddLight((int)Projectile.Center.X, (int)Projectile.Center.Y, 0f, 0.1f, 0.5f);
            float trailLength = 75f;
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
        }
        public override Color? GetAlpha(Color lightColor)
        {
            switch ((int)Projectile.ai[0])
            {
                case 0:
                    return new Color(255, 105, 45, 0);
                case 1:
                    return new Color(100, 255, 100, 0);
                case 2:
                    return new Color(73, 255, 251, 0);
                case 3:
                    return new Color(219, 112, 255, 0);
                default:
                    return new Color(219, 112, 255, 0);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));

            Rectangle value7 = new Rectangle((int)Main.screenPosition.X - 500, (int)Main.screenPosition.Y - 500, Main.screenWidth + 1000, Main.screenHeight + 1000);
            float num148 = (float)(TextureAssets.Projectile[Projectile.type].Value.Width - Projectile.width) * 0.5f + (float)Projectile.width * 0.5f;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            if (Projectile.getRect().Intersects(value7))
            {
                Vector2 value8 = new Vector2(Projectile.position.X - Main.screenPosition.X + num148, Projectile.position.Y - Main.screenPosition.Y + (float)(Projectile.height / 2) + Projectile.gfxOffY);
                float num173 = 100f;
                float scaleFactor = 3f;
                if (Projectile.ai[1] == 1f)
                {
                    num173 = (float)((int)Projectile.localAI[0]);
                }
                for (int num174 = 1; num174 <= (int)Projectile.localAI[0]; num174++)
                {
                    Vector2 value9 = Vector2.Normalize(Projectile.velocity) * (float)num174 * scaleFactor;
                    Color color32 = Projectile.GetAlpha(color25);
                    color32 *= (num173 - (float)num174) / num173;
                    color32.A = 0;
                    EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, value8 - value9, null, color32, Projectile.rotation, new Vector2(num148, (float)(Projectile.height / 2)), Projectile.scale, spriteEffects, 0f);
                }
            }
            return false;
        }
    }
}