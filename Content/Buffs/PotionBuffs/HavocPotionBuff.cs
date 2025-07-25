using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class HavocPotionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            // DisplayName.SetDefault("Havoc");
            // Description.SetDefault("Spawnrates increased by 4x");
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}