using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class Cuddled : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) *= 0.8f;
            player.lifeRegen = +2;
        }
    }
}