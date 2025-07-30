using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class LamentExplosion : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                Projectile.localAI[0] += 1f;
            }
            Projectile.ai[0] += 1f;
            float num467 = 25f;
            if (Projectile.ai[0] > 180f)
            {
                num467 -= (Projectile.ai[0] - 180f) / 2f;
            }
            if (num467 <= 0f)
            {
                num467 = 0f;
                Projectile.Kill();
            }
            int num468 = 0;
            while ((float)num468 < num467)
            {
                if (Main.rand.Next(2) == 0)
                {
                    float num469 = (float)Main.rand.Next(-10, 11);
                    float num470 = (float)Main.rand.Next(-10, 11);
                    float num471 = (float)Main.rand.Next(3, 9);
                    float num472 = (float)Math.Sqrt((double)(num469 * num469 + num470 * num470));
                    num472 = num471 / num472;
                    num469 *= num472;
                    num470 *= num472;
                    int num473 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 113, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num473].noGravity = true;
                    Main.dust[num473].position.X = Projectile.Center.X;
                    Main.dust[num473].position.Y = Projectile.Center.Y;
                    Dust var_2_157AD_cp_0_cp_0 = Main.dust[num473];
                    var_2_157AD_cp_0_cp_0.position.X = var_2_157AD_cp_0_cp_0.position.X + (float)Main.rand.Next(-10, 11);
                    Dust var_2_157D8_cp_0_cp_0 = Main.dust[num473];
                    var_2_157D8_cp_0_cp_0.position.Y = var_2_157D8_cp_0_cp_0.position.Y + (float)Main.rand.Next(-10, 11);
                    Main.dust[num473].velocity.X = num469;
                    Main.dust[num473].velocity.Y = num470;
                    int num3 = num468;
                    num468 = num3 + 1;
                }
            }
            return;
        }
    }
}