using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.MinionBuffs
{
    public class CrystallineEntityBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DisarrayEntity>()] > 0)
            {
                modPlayer.crystalEntity = true;
            }
            if (!modPlayer.crystalEntity)
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