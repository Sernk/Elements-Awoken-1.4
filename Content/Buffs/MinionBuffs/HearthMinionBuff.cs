using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Buffs.MinionBuffs
{
    public class HearthMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.ownedProjectileCounts[ProjectileType<HearthMinion>()] > 0)  modPlayer.hearthMinion = true;
            if (!modPlayer.hearthMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else player.buffTime[buffIndex] = 18000;
        }
    }
}