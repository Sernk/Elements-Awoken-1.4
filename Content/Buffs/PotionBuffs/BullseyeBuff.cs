using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class BullseyeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        { 
            player.GetCritChance(DamageClass.Magic) += 15;
            player.GetCritChance(DamageClass.Melee) += 15;
            player.GetCritChance(DamageClass.Ranged) += 15;
            player.GetCritChance(DamageClass.Ranged) += 15;
            player.GetDamage(DamageClass.Ranged) *= 1.25f;
        }
    }
}