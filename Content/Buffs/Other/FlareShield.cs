using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Other
{
    public class FlareShield : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.FlareShield>()] == 0)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.FlareShield>(), 0, 0f, player.whoAmI);
            }
        }
    }
}