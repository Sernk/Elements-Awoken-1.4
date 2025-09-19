using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.EAPlayer;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ImpishWave : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Impish Wave");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 60);
            target.AddBuff(ModContent.BuffType<ImpishCurse>(), 300);
            for (int l = 0; l < 5; l++)
            {
                int dir = l == 1 ? -1 : 1;
                Projectile.NewProjectile(EAU.Proj(Projectile), target.Center.X, target.Center.Y - 200 * dir, 0f, 8 * dir, ModContent.ProjectileType<ImpishFireball>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 6, damageType: "magic");
        }
    }
}