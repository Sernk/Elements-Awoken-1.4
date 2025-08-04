using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class FrostShield : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) *= 1.5f;
            player.GetDamage(DamageClass.Ranged) *= 1.5f;
            player.GetDamage(DamageClass.Magic) *= 1.5f;
            player.GetDamage(DamageClass.Summon) *= 1.5f;
            player.statDefense += 20;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.InfinityGauntlet.FrostShield>()] == 0)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.InfinityGauntlet.FrostShield>(), 100, 0f, player.whoAmI);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.InfinityGauntlet.FrostShield2>()] == 0)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.InfinityGauntlet.FrostShield2>(), 100, 0f, player.whoAmI);
            }
        }
    }
}