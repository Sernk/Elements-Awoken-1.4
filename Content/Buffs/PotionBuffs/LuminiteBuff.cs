using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class LuminiteBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        { 
            player.statDefense += 8;
            player.GetDamage(DamageClass.Melee) += 0.15f;
            player.GetDamage(DamageClass.Throwing) += 0.15f;
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.GetDamage(DamageClass.Magic) += 0.15f;
            player.GetDamage(DamageClass.Summon) += 0.15f;
        }
    }
}