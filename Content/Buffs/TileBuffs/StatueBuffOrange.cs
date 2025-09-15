using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.TileBuffs
{
    public class StatueBuffOrange : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) *= 1.15f;
            player.GetDamage(DamageClass.Magic) *= 1.15f;
            player.GetDamage(DamageClass.Summon) *= 1.15f;
            player.GetDamage(DamageClass.Ranged) *= 1.15f;
            player.GetDamage(DamageClass.Throwing) *= 1.15f;

            player.GetModPlayer<MyPlayer>().damageTaken *= 1.5f;
            player.potionDelayTime = (int)(player.potionDelayTime * 1.25);
            player.restorationDelayTime = (int)(player.restorationDelayTime * 1.25);
        }
    }
}