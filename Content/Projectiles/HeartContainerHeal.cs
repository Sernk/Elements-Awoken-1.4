using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class HeartContainerHeal : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            int num508 = (int)Projectile.ai[0];
            float num509 = 4f;
            Vector2 vector33 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
            float num510 = Main.player[num508].Center.X - vector33.X;
            float num511 = Main.player[num508].Center.Y - vector33.Y;
            float num512 = (float)Math.Sqrt((double)(num510 * num510 + num511 * num511));
            float num513 = num512;
            if (num512 < 50f && Projectile.position.X < Main.player[num508].position.X + (float)Main.player[num508].width && Projectile.position.X + (float)Projectile.width > Main.player[num508].position.X && Projectile.position.Y < Main.player[num508].position.Y + (float)Main.player[num508].height && Projectile.position.Y + (float)Projectile.height > Main.player[num508].position.Y)
            {
                if (Projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech)
                {
                    int num514 = (int)Projectile.ai[1];
                    Main.player[num508].HealEffect(num514, false);
                    Player player = Main.player[num508];
                    player.statLife += num514;
                    if (Main.player[num508].statLife > Main.player[num508].statLifeMax2)
                    {
                        Main.player[num508].statLife = Main.player[num508].statLifeMax2;
                    }
                    NetMessage.SendData(66, -1, -1, null, num508, (float)num514, 0f, 0f, 0, 0, 0);
                }
                Projectile.Kill();
            }
            num512 = num509 / num512;
            num510 *= num512;
            num511 *= num512;
            Projectile.velocity.X = (Projectile.velocity.X * 15f + num510) / 16f;
            Projectile.velocity.Y = (Projectile.velocity.Y * 15f + num511) / 16f;
            {
                for (int i = 0; i < 5; i += 1)
                {
                    float num520 = Projectile.velocity.X * 0.2f * i;
                    float num521 = (0f - Projectile.velocity.Y * 0.2f) * i;
                    Vector2 position144 = new Vector2(Projectile.position.X, Projectile.position.Y);
                    int width117 = Projectile.width;
                    int height117 = Projectile.height;
                    int num522 = Dust.NewDust(position144, width117, height117, 90, 0f, 0f, 100, default(Color), 1.3f);
                    Main.dust[num522].noGravity = true;
                    Main.dust[num522].velocity *= 0f;
                    Main.dust[num522].position.X -= num520;
                    Main.dust[num522].position.Y -= num521;
                }
            }
        }
    }
}