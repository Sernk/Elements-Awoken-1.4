using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class TemplesWrathArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.scale = 1f;
            Projectile.penetrate = 1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 100);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch)];
                dust.velocity *= 0.6f;
                dust.scale *= 0.6f;
                dust.noGravity = true;
            }
        }
    }
}