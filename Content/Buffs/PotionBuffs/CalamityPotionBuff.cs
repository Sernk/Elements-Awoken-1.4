﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.PotionBuffs
{
    public class CalamityPotionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            // DisplayName.SetDefault("Calamity");
            // Description.SetDefault("Spawnrates increased by 12.5x");
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}