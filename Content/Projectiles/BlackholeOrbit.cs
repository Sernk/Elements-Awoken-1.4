using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BlackholeOrbit : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public bool reset = false;
        public int distance = 75;
        public float speed = 5f;
        public int dustType = 6;
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 100000;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            if (!reset)
            {
                distance = Main.rand.Next(75, 190);
                speed = Main.rand.NextFloat(1f, 3f);
                switch (Main.rand.Next(2))
                {
                    case 0:
                        dustType = EAU.PinkFlame;
                        break;
                    case 1:
                        dustType = 54;
                        break;
                    default: break;
                }
                reset = true;
            }
            for (int k = 0; k < 3; k++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, default(Color), 1f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            Projectile parent = Main.projectile[(int)Projectile.ai[1]];

            Projectile.localAI[0] += 0.05f;
            double distAdd = Math.Sin(Projectile.localAI[0]);

            Projectile.ai[0] += speed;
            double rad = Projectile.ai[0] * (Math.PI / 180);
            float targetX = parent.Center.X - (int)(Math.Cos(rad) * (distance + distAdd * 20));
            float targetY = parent.Center.Y - (int)(Math.Sin(rad) * (distance + distAdd * 20));

            Vector2 toTarget = new Vector2(targetX - Projectile.Center.X, targetY - Projectile.Center.Y);
            toTarget.Normalize();
            Projectile.velocity += toTarget * speed / 4f;
            if (Vector2.Distance(Projectile.Center, parent.Center) >= distance + 100)
            {
                Projectile.velocity *= 0.98f;
            }
            if (!parent.active)
            {
                Projectile.Kill();
            }           
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.realLife == -1)
            {
                target.immune[Projectile.owner] = 5;
            }
            target.AddBuff(ModContent.BuffType<ExtinctionCurse>(), 180, false);
        }
    }
}