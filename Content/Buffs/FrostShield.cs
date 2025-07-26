using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class FrostShield : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frost Shield");
            // Description.SetDefault("");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) *= 1.5f;
            player.GetDamage(DamageClass.Ranged) *= 1.5f;
            player.GetDamage(DamageClass.Magic) *= 1.5f;
            player.GetDamage(DamageClass.Summon) *= 1.5f;
            player.statDefense += 20;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.InfinityGauntlet.FrostShield>()] == 0)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.InfinityGauntlet.FrostShield>(), 100, 0f, player.whoAmI);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.InfinityGauntlet.FrostShield2>()] == 0)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.InfinityGauntlet.FrostShield2>(), 100, 0f, player.whoAmI);
            }
        }
    }
}