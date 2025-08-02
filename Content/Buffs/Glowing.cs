using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class Glowing : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            Lighting.AddLight(player.Center, 5f, 5f, 5f);
        }
    }
}