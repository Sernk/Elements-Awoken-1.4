using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Other
{
    public class ChaosBannerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}