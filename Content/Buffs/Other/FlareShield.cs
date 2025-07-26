using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Other
{
    public class FlareShield : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flare Shield");
            // Description.SetDefault("");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.FlareShield>()] == 0)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.FlareShield>(), 0, 0f, player.whoAmI);
            }
        }
    }
}