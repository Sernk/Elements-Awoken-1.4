using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Bullets
{
    public class BlightedBulletP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Corroding>(), 300, false);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.3f, 0.9f, 0.6f);

            if (Main.rand.Next(220) == 0)
            {
                Vector2 cloudVel = new Vector2(-Projectile.velocity.X * 0.3f, -Projectile.velocity.Y * 0.3f);
                cloudVel = cloudVel.RotatedByRandom(MathHelper.ToRadians(8));
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, -Projectile.velocity.X * 0.1f, -Projectile.velocity.Y * 0.1f, ModContent.ProjectileType<BlightCloud>(), (int)(Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner, 0f, 0f);
            }

            int dustLength = 2;
            for (int l = 0; l < dustLength; l++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 74)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / dustLength * (float)l;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}