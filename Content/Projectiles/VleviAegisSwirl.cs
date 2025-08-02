using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.EASystem;

namespace ElementsAwoken.Content.Projectiles
{
    public class VleviAegisSwirl : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
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

            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Firework_Pink, 0f, 0f, 100, default(Color), 1.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0f;
            Main.dust[dust].customData = parent;

            if (modPlayer.vleviAegisBoost <= 0)
            {
                Projectile.Kill();
            }
            if (!parent.active || parent.dead)
            {
                Projectile.Kill();
            }
            Projectile.ai[0] += Projectile.localAI[0] == 0 ? -5f : 5f;
            int distance = Projectile.localAI[0] == 0 ? 40 : 30;
            double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
            Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
            Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;
        }
    }
}
    