using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class Invincibility : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.shadowDodge = true;
            player.immune = true;
            Lighting.AddLight(player.Center, 0.5f, 0.4f, 0.1f);
        }
    }
}