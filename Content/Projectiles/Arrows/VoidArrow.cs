using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class VoidArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.scale = 1f;
            Projectile.penetrate = 1;
        }
        public override void AI()
        { 
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127)];
                dust.velocity *= 0.6f;
                dust.scale *= 0.6f;
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(EAU.HandsOfDespair, 180);
            int numberProjectiles = 2;
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                int num1 = Main.rand.Next(-30, 30);
                int num2 = Main.rand.Next(300, 500);
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + num1, Projectile.Center.Y - num2, 0, 20, ModContent.ProjectileType<CrimsonSkyLaser>(), Projectile.damage, 0, Projectile.owner);
            }
        }
    }
}