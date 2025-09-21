using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.MinionBuffs
{
    public class EnchantedTrio : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<EnchantedTrio0>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<EnchantedTrio1>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<EnchantedTrio2>()] > 0)
            {
                modPlayer.enchantedTrio = true;
            }
            if (!modPlayer.enchantedTrio)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}