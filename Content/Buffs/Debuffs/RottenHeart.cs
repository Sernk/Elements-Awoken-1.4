using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class RottenHeart : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            EAU.Longer(Type);
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 = (int)(player.statLifeMax2 * 0.9f);
        }
    }
}