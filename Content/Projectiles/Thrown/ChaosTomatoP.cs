using ElementsAwoken.Content.Items.Pets;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class ChaosTomatoP : ModProjectile
    {  	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X;
            if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
        public override void AI()
        {
            Projectile.rotation += 0.02f;
            Projectile.ai[0]++;
            if (Projectile.ai[0] < 60)
            {
                if (Projectile.velocity.Y < -2) Projectile.velocity.Y -= 0.02f;
                Projectile.velocity.X *= 0.97f;
            }
            else
            {
                Projectile.velocity *= 0.97f;
            }
            if (Projectile.ai[0] > 20)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.Hitbox.Intersects(Projectile.Hitbox))
                    {
                        Vector2 toTarget = new Vector2(Projectile.Center.X - p.Center.X, Projectile.Center.Y - p.Center.Y);
                        toTarget.Normalize();
                        Projectile.velocity += toTarget * 0.75f;
                    }
                }
            }
            int maxDist = 20;
            for (int i = 0; i < 6; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset - Vector2.One * 4, 0, 0, 127, 0, 0, 100)];
                dust.noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            int item = Item.NewItem(EAU.Proj(Projectile), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<AzanaChibi>());
            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
        }
    }
}