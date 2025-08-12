using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Infernace
{
    public class InfernaceFire : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GreekFire1);
            AIType = ProjectileID.GreekFire1;
            Projectile.scale = 1f;
            Projectile.hostile = true;
            Projectile.timeLeft = 180;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 80, false);
        }
    }
}