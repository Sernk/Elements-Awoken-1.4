using ElementsAwoken.Content.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class ChaosShield : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ChaosRingShield>()] == 0)
            {
                Projectile.NewProjectile(Const.Players(player), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<ChaosRingShield>(), 100, 0f, player.whoAmI);
            }
        }
    }
}