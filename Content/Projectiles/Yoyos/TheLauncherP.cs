using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class TheLauncherP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 275f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 12f;
        }
        public override void AI()
        {
            if (Main.rand.Next(7) == 0)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.NextFloat(-12.5f, 12.5f), Main.rand.NextFloat(-12.5f, 12.5f), ModContent.ProjectileType<TheLauncherBolt>(), Projectile.damage, 0, Projectile.owner);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.velocity.Y -= Main.rand.Next(1, 20) * target.knockBackResist;
            target.velocity.X += Main.rand.Next(-20, 20) * target.knockBackResist;
        }
    }
}