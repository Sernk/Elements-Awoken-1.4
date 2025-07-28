using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class RailgunBeam : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 20;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.velocity.X != Projectile.velocity.X)
            {
                Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
                Projectile.velocity.X = -Projectile.velocity.X;
            }
            if (Projectile.velocity.Y != Projectile.velocity.Y)
            {
                Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
                Projectile.velocity.Y = -Projectile.velocity.Y;
            }
            float oldAI0 = Projectile.ai[0];
            Projectile.ai[0] += (float)(Math.PI / 10);
            int dustLength = 4;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 220)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.color = Color.Aqua;
            }
            for (int j = -1; j < 2; j += 2)
            {
                int numDust = 2;
                for (int i = 0; i < numDust; i++)
                {
                    float ai = i == 0 ? oldAI0 : Projectile.ai[0];
                    Vector2 pos = i == 0 ? Projectile.position - (Projectile.position - Projectile.oldPosition) : Projectile.position;
                    pos += new Vector2(Projectile.width,Projectile.height) / 2;
                    float Y = ((float)Math.Sin(ai) * 20) * j;
                    Vector2 dustPos = new Vector2(Y, 0);
                    dustPos = dustPos.RotatedBy((double)Projectile.rotation, default(Vector2));

                    Dust dust = Main.dust[Dust.NewDust(pos + dustPos - Vector2.One * 5f, 2, 2, 220)];
                    dust.velocity = Vector2.Zero;
                    dust.noGravity = true;
                    dust.color = Color.Aqua;
                }
            }
        }
    }
}