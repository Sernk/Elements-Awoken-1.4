using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class NeovirtuoShieldSwirl2 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public float spinAi = 0f;
        public bool reset = true;
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 4000;
            Projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(Projectile.ai[0] * 8, 0);
            Projectile parent = Main.projectile[(int)Projectile.ai[1]];
            if (reset)
            {
                if (Projectile.ai[0] == 1)
                {
                    spinAi -= 0.15f;
                }
                if (Projectile.ai[0] == 2)
                {
                    spinAi -= 0.3f;
                }
                if (Projectile.ai[0] == 3)
                {
                    spinAi -= 0.45f;
                }
                if (Projectile.ai[0] == 4)
                {
                    spinAi -= 0.6f;
                }
                if (Projectile.ai[0] == 5)
                {
                    spinAi -= 0.75f;
                }
                if (Projectile.ai[0] == 6)
                {
                    spinAi -= 0.9f;
                }
                if (Projectile.ai[0] == 7)
                {
                    spinAi -= 1.05f;
                }
                if (Projectile.ai[0] == 8)
                {
                    spinAi -= 1.2f;
                }
                if (Projectile.ai[0] == 9)
                {
                    spinAi -= 1.35f;
                }
                if (Projectile.ai[0] == 10)
                {
                    spinAi -= 1.5f;
                }
                reset = false;
                spinAi += 3f;
            }
            spinAi += 0.03f;
            Projectile.Center = parent.Center + offset.RotatedBy(spinAi + Projectile.ai[1] * (Math.PI * 2 / 8));
            if (!parent.active)
            {
                Projectile.Kill();
            }
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 135, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 4.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.4f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 2;
        }
    }
}