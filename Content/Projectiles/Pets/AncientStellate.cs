using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Pets
{
    public class AncientStellate : ModProjectile
    {
        private float aiState
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = -1;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Main.projPet[Projectile.type] = true;
            Projectile.tileCollide = false;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (aiState == 0)
            {
                Projectile.frame = 0;
            }
            else
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 4)
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                    if (Projectile.frame > 7)
                        Projectile.frame = 0;
                }
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead) modPlayer.stellate = false;
            if (modPlayer.stellate)  Projectile.timeLeft = 2;

            if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
            {
                Projectile.ai[0] = 1f;
            }
            float speed = 6f;
            if (Projectile.ai[0] == 1f)
            {
                speed = 15f;
            }
            if (aiState == 0)
            {
                if (Projectile.velocity.Y < 0) Projectile.velocity.Y *= 0.99f;           
                    Projectile.velocity.Y += 0.06f;
                if (Projectile.Center.Y > player.Center.Y + 50) aiState = 1;
            }
            else
            {
                if (Projectile.velocity.Y > -4) Projectile.velocity.Y -= 0.5f;
                if (Projectile.Center.Y < player.Center.Y - 75) aiState = 0;
            }
            float toVelX = player.Center.X -( 50 * player.direction) - Projectile.Center.X;
            if (Math.Abs(toVelX) < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.ai[0] = 0f;
                Projectile.netUpdate = true;
            }
            if (Vector2.Distance(player.Center,Projectile.Center) > 2000f) Projectile.Center = player.Center;
            if (Math.Abs(toVelX) > 48f)
            {
                toVelX = Math.Sign(toVelX) * speed;
                float temp = 40 / 2f;
                Projectile.velocity.X = (Projectile.velocity.X * temp + toVelX) / (temp + 1);
            }
            else
            {
                Projectile.velocity.X *= (float)Math.Pow(0.9, 40.0 / 40);
            }

            Projectile.rotation = Projectile.velocity.X * 0.05f;
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