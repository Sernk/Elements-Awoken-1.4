using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.RegarothP
{
    public class RegarothBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void AI()
        {
            Main.projFrames[Projectile.type] = 4;

            Projectile.velocity.X *= 0.99f;
            Projectile.velocity.Y *= 0.99f;
            // create dusts in a circle shape
            if (Main.rand.Next(6) == 0 && !ModContent.GetInstance<Config>().lowDust)
            {
                int dustType = Main.rand.Next(2) == 0 ? 135 : 164;
                Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width * 0.5f, Projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, dustType, Vector2.Zero);
                dust.noGravity = true;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.Kill();
            ProjectileUtils.HostileExplosion(Projectile, new int[] { 135, 164}, Projectile.damage);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}