using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.EAPlayer;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.MinionBuffs
{
    public class AwakenedMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<WokeMinion>()] > 0)
            {
                modPlayer.wokeMinion = true;
            }
            if (!modPlayer.wokeMinion)
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