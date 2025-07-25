using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Other
{
    public class ChaosBannerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            // DisplayName.SetDefault("Chaos Banner");
            // Description.SetDefault("Spawnrates increased by 5x");
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}