using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ZapMasterLightning : ModProjectile
    {
        public Vector2 velocity = new Vector2();
        public int[] hitCounter = new int[201];

        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 5;
            Projectile.extraUpdates = 100;
            Projectile.timeLeft = 100;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hitCounter[target.whoAmI]++;

            Vector2 loc = new Vector2();

            float closestEntity = 200f;
            bool home = false;
            int whoAmI = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, nPC.Center, 1, 1))
                {
                    float dist = Math.Abs(Projectile.Center.X - nPC.Center.X) + Math.Abs(Projectile.Center.Y - nPC.Center.Y);
                    if (dist < closestEntity && !CheckForNearbyLessHitNPCs(nPC) && nPC.whoAmI != whoAmI)
                    {
                        closestEntity = dist;
                        loc = nPC.Center;
                        home = true;
                        whoAmI = nPC.whoAmI;
                    }
                }
            }
            if (home)
            {
                float speed = 7f;
                double angle = Math.Atan2(loc.Y - Projectile.position.Y, loc.X - Projectile.position.X);
                velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
            }
            else Projectile.Kill();
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
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 7)
            {
                int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
                for (int i = 0; i < dustLength; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 226)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / dustLength * (float)i;
                    dust.noGravity = true;
                }
            }
            if (velocity == Vector2.Zero) velocity = Projectile.velocity;
             Projectile.velocity = velocity.RotatedByRandom(.75);
           
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