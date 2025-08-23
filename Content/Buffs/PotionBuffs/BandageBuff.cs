using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class BandageBuff : ModBuff
    {
        public float healTimer = 8f;
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (healTimer > 0f)
            {
                healTimer -= 1f;
            }
            if (healTimer == 0f)
            {
                player.statLife += 1;
                player.HealEffect(1);
                healTimer = 8f;
            }
        }
    }
}