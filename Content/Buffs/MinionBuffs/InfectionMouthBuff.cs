using ElementsAwoken.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;
using ElementsAwoken.EASystem.Global;

namespace ElementsAwoken.Content.Buffs.MinionBuffs
{
    public class InfectionMouthBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<InfectionMouthMinion>()] > 0)
            {
                modPlayer.azanaMinions = true;
            }
            if (!modPlayer.azanaMinions)
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