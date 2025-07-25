using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Other
{
    public class CalamityBannerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            // DisplayName.SetDefault("Calamity Banner");
            // Description.SetDefault("Spawnrates increased by 7.5x");
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}