using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class ExtinctionCurseImbue : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.meleeBuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().extinctionCurseImbue = true;
        }
    }
}