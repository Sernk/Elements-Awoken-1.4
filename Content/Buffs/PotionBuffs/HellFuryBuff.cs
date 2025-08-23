using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class HellFuryBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) *= 1.2f;
            player.GetDamage(DamageClass.Ranged) *= 1.2f;
            player.GetDamage(DamageClass.Magic) *= 1.2f;
            player.GetDamage(DamageClass.Summon) *= 1.2f;
            player.AddBuff(BuffID.WeaponImbueFire, 1);
        }
    }
}