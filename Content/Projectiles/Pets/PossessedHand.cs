using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Pets
{
    public class PossessedHand : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Main.projPet[Projectile.type] = true;
            Projectile.tileCollide = false;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead)
            {
                modPlayer.possessedHand = false;
            }
            if (modPlayer.possessedHand)
            {
                Projectile.timeLeft = 2;
            }

            float num535 = Projectile.position.X;
            float num536 = Projectile.position.Y;

            float num546 = 8f;
            if (Projectile.ai[0] == 1f)
            {
                num546 = 12f;
            }
            Vector2 vector42 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
            float num547 = player.Center.X - vector42.X;
            float num548 = player.Center.Y - vector42.Y - 60f;
            float num549 = (float)Math.Sqrt((double)(num547 * num547 + num548 * num548));
            if (num549 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.ai[0] = 0f;
            }
            if (num549 > 2000f)
            {
                Projectile.position.X = player.Center.X - (float)(Projectile.width / 2);
                Projectile.position.Y = player.Center.Y - (float)(Projectile.width / 2);
            }
            if (num549 > 70f)
            {
                num549 = num546 / num549;
                num547 *= num549;
                num548 *= num549;
                Projectile.velocity.X = (Projectile.velocity.X * 20f + num547) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num548) / 21f;
            }
            else
            {
                if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                {
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
                Projectile.velocity *= 1.01f;
            }
            Projectile.rotation = Projectile.velocity.X * 0.2f;
            if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
            {
                Projectile.spriteDirection = -Projectile.direction;
                return;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}