using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Prompts
{
    public class Psychosis : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            EAU.CanBeCleared(Type);
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 10;
            player.GetDamage(DamageClass.Melee) *= 0.95f;
            player.GetDamage(DamageClass.Summon) *= 0.95f;
            player.GetDamage(DamageClass.Ranged) *= 0.95f;
            player.GetDamage(DamageClass.Magic) *= 0.95f;
            player.GetDamage(DamageClass.Throwing) *= 0.95f;
        }
    }
}