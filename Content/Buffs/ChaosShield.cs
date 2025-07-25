using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.Content.Projectiles;

namespace ElementsAwoken.Content.Buffs
{
    public class ChaosShield : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Shield");
            // Description.SetDefault("");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ChaosRingShield>()] == 0)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<ChaosRingShield>(), 100, 0f, player.whoAmI);
            }
        }
    }
}