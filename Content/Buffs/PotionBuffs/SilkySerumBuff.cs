using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class SilkySerumBuff : ModBuff
	{
		public override void Update(Player player, ref int buffIndex)
		{
            player.endurance += 0.1f;
			player.GetModPlayer<MyPlayer>().puffFall = true;
		}
	}
}