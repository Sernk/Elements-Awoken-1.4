using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.TileBuffs
{
    public class StatueBuffAmadis : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) *= 1.15f;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.15f;

            player.statLifeMax2 = (int)(player.statLifeMax2 * 0.7f) + 1;
        }
    }
}