using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class VoidJellyBuff : ModBuff
    {
        public override void SetStaticDefaults() => Main.buffNoTimeDisplay[Type] = false;
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) *= 1.15f;
            player.GetDamage(DamageClass.Ranged) *= 1.15f;
            player.GetDamage(DamageClass.Magic) *= 1.15f;
            player.GetDamage(DamageClass.Summon) *= 1.15f;
        }
    }
}