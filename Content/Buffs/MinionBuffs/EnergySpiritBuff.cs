using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.MinionBuffs
{
    public class EnergySpiritBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<EnergySpirit>()] > 0)
            {
                modPlayer.energySpirit = true;
            }
            if (!modPlayer.energySpirit)
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