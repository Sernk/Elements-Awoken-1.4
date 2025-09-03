using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class HeartyMeal : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<MyPlayer>().generalTimer % 60 == 0)
            { 
                player.statLife += 2;
                player.HealEffect(2);
            }
        }
    }
}