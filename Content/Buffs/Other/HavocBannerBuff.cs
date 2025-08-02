using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Other
{
    public class HavocBannerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}