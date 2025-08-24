using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Pets.Wyvern
{
    public class WyvernTail : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projPet[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.timeLeft *= 5;
        }
        public override void AI()
        {
            Player player10 = Main.player[Projectile.owner];
            if (!player10.active)
            {
                Projectile.active = false;
                return;
            }

            int num1049 = 10;
            Vector2 parCenter = Vector2.Zero;
            float parRot = 0f;
            float scaleFactor16 = 0f;
            float scaleFactor17 = 1f;
            if (Projectile.ai[1] == 1f)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            Projectile parent = Main.projectile[(int)Projectile.ai[0]];
            if ((int)Projectile.ai[0] >= 0 && parent.active)
            {
                parCenter = parent.Center;
                parRot = parent.rotation;
                scaleFactor17 = MathHelper.Clamp(parent.scale, 0f, 50f);
                scaleFactor16 = 16f;
                parent.localAI[0] = Projectile.localAI[0] + 1f;
            }
            else
            {
                Projectile.Kill();
                return;
            }
            Projectile.velocity = Vector2.Zero;
            Vector2 vector151 = parCenter - Projectile.Center;
            if (parRot != Projectile.rotation)
            {
                float num1070 = MathHelper.WrapAngle(parRot - Projectile.rotation);
                vector151 = vector151.RotatedBy((double)(num1070 * 0.1f), default(Vector2));
            }
            Projectile.rotation = vector151.ToRotation() + 1.57079637f;
            Projectile.position = Projectile.Center;
            Projectile.scale = scaleFactor17;
            Projectile.width = (Projectile.height = (int)((float)num1049 * Projectile.scale));
            Projectile.Center = Projectile.position;
            if (vector151 != Vector2.Zero)
            {
                Projectile.Center = parCenter - Vector2.Normalize(vector151) * 1.4f * scaleFactor16 * scaleFactor17;
            }
            Projectile.spriteDirection = ((vector151.X > 0f) ? 1 : -1);
            return;
        }
    }
}
