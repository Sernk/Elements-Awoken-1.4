using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class VilePower : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().vilePower = true;
            player.GetDamage(DamageClass.Generic) *= 1.05f;
        }
    }
}