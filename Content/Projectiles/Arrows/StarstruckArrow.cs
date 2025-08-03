using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class StarstruckArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 3;
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 600;
            AIType = 1;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starstruck");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var p = Const.Proj(Projectile);
            var type = ModContent.ProjectileType<StarstruckBeam>();
            Projectile.NewProjectile(p, Projectile.Center.X, Projectile.Center.Y + -300, 0, 15, type, Projectile.damage, 0, Projectile.owner);
            Projectile.NewProjectile(p, Projectile.Center.X, Projectile.Center.Y + 300, 0, -15, type, Projectile.damage, 0, Projectile.owner);
            Projectile.NewProjectile(p, Projectile.Center.X + 300, Projectile.Center.Y, -15, 0, type, Projectile.damage, 0, Projectile.owner);
            Projectile.NewProjectile(p, Projectile.Center.X + -300, Projectile.Center.Y, 15, 0, type, Projectile.damage, 0, Projectile.owner);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 5; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 164)];
                dust.velocity *= 0.6f;
                dust.scale *= 0.6f;
                dust.noGravity = true;
                Dust dust2 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 135)];
                dust2.velocity *= 0.6f;
                dust2.scale *= 0.6f;
                dust2.noGravity = true;
            }
        }
    }
}