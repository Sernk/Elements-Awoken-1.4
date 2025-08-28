using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Snowflake : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.MagicMissile);
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = 9;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 70;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snowflake");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 200);
        }
    }
}