using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class OrionsBeltGalaxy : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 220, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 100);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            Projectile parent = Main.projectile[(int)Projectile.ai[1]];
            Vector2 whipCenter = parent.position + parent.velocity;

            Projectile.ai[0] += 10f;
            int distance = 50;
            double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
            Projectile.position.X = whipCenter.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
            Projectile.position.Y = whipCenter.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;

            if (!parent.active)
            {
                Projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 9;
        }
    }
}