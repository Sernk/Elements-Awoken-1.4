using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class WindsBlessing : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 1.6f;
        }
    }
}