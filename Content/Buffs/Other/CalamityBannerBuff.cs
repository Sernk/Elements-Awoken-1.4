using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Other
{
    public class CalamityBannerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}