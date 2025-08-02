using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ChaosRingSwirl : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10000;
            Projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(40, 0);
            Projectile parent = Main.projectile[(int)Projectile.ai[1]];
            Projectile.ai[0] += 0.1f;
            Projectile.position = parent.position + offset.RotatedBy(Projectile.ai[0] + Projectile.ai[1] * (Math.PI * 2 / 8));

            if (parent.active == false)
            {
                Projectile.Kill();
            }
            for (int l = 0; l < 5; l++)
            {
                float num95 = Projectile.velocity.X / 3f * (float)l;
                float num96 = Projectile.velocity.Y / 3f * (float)l;
                int num97 = 4;
                int num98 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num97, Projectile.position.Y + (float)num97), Projectile.width - num97 * 2, Projectile.height - num97 * 2, 127, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num98].noGravity = true;
                Dust dust = Main.dust[num98];
                dust.velocity *= 0.1f;
                dust = Main.dust[num98];
                dust.velocity += Projectile.velocity * 0.1f;
                Dust var_2_4829_cp_0_cp_0 = Main.dust[num98];
                var_2_4829_cp_0_cp_0.position.X = var_2_4829_cp_0_cp_0.position.X - num95;
                Dust var_2_4843_cp_0_cp_0 = Main.dust[num98];
                var_2_4843_cp_0_cp_0.position.Y = var_2_4843_cp_0_cp_0.position.Y - num96;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num99 = 4;
                int num100 = Dust.NewDust(new Vector2(Projectile.position.X + (float)num99, Projectile.position.Y + (float)num99), Projectile.width - num99 * 2, Projectile.height - num99 * 2, 127, 0f, 0f, 100, default(Color), 0.6f);
                Dust dust = Main.dust[num100];
                dust.velocity *= 0.25f;
                dust = Main.dust[num100];
                dust.velocity += Projectile.velocity * 0.5f;
            }
        }    
    }
}