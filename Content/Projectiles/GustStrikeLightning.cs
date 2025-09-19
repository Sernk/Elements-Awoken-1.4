using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class GustStrikeLightning : ModProjectile
    {
        public Vector2 velocity = new();
        public int[] hitCounter = new int[201];
        public int targetted = -1;
        public int tileCollideTimer = 0;

        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 200;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Strange Ukulele");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hitCounter[target.whoAmI]++;
        }
        public override void AI()
        {
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
            int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
            }
            if (velocity == Vector2.Zero) velocity = Projectile.velocity;

            float targetX = Projectile.Center.X;
            float targetY = Projectile.Center.Y;
            float closestEntity = 200f;
            bool home = false;
            targetted = -1;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    float dist = Math.Abs(Projectile.Center.X - nPC.Center.X) + Math.Abs(Projectile.Center.Y - nPC.Center.Y);
                    if (dist < closestEntity && !CheckForNearbyLessHitNPCs(nPC))
                    {
                        closestEntity = dist;
                        targetX = nPC.Center.X;
                        targetY = nPC.Center.Y;
                        targetted = nPC.whoAmI;
                        home = true;
                    }
                }
            }
            if (home)
            {
                float speed = 7f;
                float goToX = targetX - Projectile.Center.X;
                float goToY = targetY - Projectile.Center.Y;
                float dist = (float)Math.Sqrt((double)(goToX * goToX + goToY * goToY));
                dist = speed / dist;
                goToX *= dist;
                goToY *= dist;
                Projectile.velocity = new Vector2((Projectile.velocity.X * 20f + goToX) / 21f, (Projectile.velocity.Y * 20f + goToY) / 21f).RotatedByRandom(.5);
                //velocity = projectile.velocity;
                return;
            }
            else
            {
                Projectile.velocity = velocity.RotatedByRandom(2.5);
            }

            if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                tileCollideTimer++;
                if (tileCollideTimer > 10) Projectile.Kill();
            }
            else tileCollideTimer = 0;
        }
        private bool CheckForNearbyLessHitNPCs(NPC currentNPC) // the worst name
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.whoAmI != currentNPC.whoAmI && nPC.CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    if (Vector2.Distance(nPC.Center, Projectile.Center) < 200 && hitCounter[i] < hitCounter[currentNPC.whoAmI])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}