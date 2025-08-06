using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using ElementsAwoken.EASystem.Global;
namespace ElementsAwoken.Content.Projectiles
{
    public class LightningCloudSwirl : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Player parent = Main.player[(int)Projectile.ai[1]];
            MyPlayer modPlayer = parent.GetModPlayer<MyPlayer>();

            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 15, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0f;
        
            if (modPlayer.lightningCloudCharge < 300)
            {
                Projectile.Kill();
            }

            Projectile.ai[0] += 3f;
            int distance = 25;
            double rad = Projectile.ai[0] * (Math.PI / 180);
            Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
            Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;

            if (!parent.active || modPlayer.lightningCloudHidden || !modPlayer.lightningCloud)
            {
                Projectile.Kill();
            }

            if (Main.netMode != 0)
            {
                if (parent.ownedProjectileCounts[Projectile.type] > 3)
                {
                    Projectile.Kill();
                }
            }
        }
    }
}