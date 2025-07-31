using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class VoidLeviathanProjTail : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            Player player10 = Main.player[Projectile.owner];
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
                return;
            }
            if (Projectile.alpha > 0)
            {
                int num3;
                for (int num1068 = 0; num1068 < 2; num1068 = num3 + 1)
                {
                    int num1069 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 135, 0f, 0f, 100, default, 2f);
                    Main.dust[num1069].noGravity = true;
                    Main.dust[num1069].noLight = true;
                    num3 = num1068;
                }
            }
            Projectile.alpha -= 42;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            Projectile.velocity = Vector2.Zero;
            Vector2 vector151 = parCenter - Projectile.Center;
            if (parRot != Projectile.rotation)
            {
                float num1070 = MathHelper.WrapAngle(parRot - Projectile.rotation);
                vector151 = vector151.RotatedBy((double)(num1070 * 0.1f), default);
            }
            Projectile.rotation = vector151.ToRotation() + 1.57079637f;
            Projectile.position = Projectile.Center;
            Projectile.scale = scaleFactor17;
            Projectile.width = Projectile.height = (int)(num1049 * Projectile.scale);
            Projectile.Center = Projectile.position;
            if (vector151 != Vector2.Zero)
            {
                Projectile.Center = parCenter - Vector2.Normalize(vector151) * scaleFactor16 * scaleFactor17;
            }
            Projectile.spriteDirection = vector151.X > 0f ? 1 : -1;

            if (!parent.active || parent.type != ModContent.ProjectileType<VoidLeviathanProjBody>() && parent.type != ModContent.ProjectileType<VoidLeviathanProjHead>() && parent.type != ModContent.ProjectileType<VoidLeviathanProjTail>())
            {
                Projectile.Kill();
            }
            return;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 31; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Const.PinkFlame, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default, 1.8f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
    }
}
