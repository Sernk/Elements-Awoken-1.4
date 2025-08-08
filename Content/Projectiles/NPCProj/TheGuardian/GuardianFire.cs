using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.TheGuardian
{
    public class GuardianFire : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GreekFire1);
            AIType = ProjectileID.GreekFire1;
            Projectile.scale = 1f;
            Projectile.hostile = true;
            Projectile.timeLeft = 240;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
    }
}