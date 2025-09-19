using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Yoyos
{
    public class MarsP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 205f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 8f;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 5f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => target.AddBuff(BuffID.Poisoned, 180, false);
    }
}