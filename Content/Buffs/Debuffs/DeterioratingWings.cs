﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.EASystem;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class DeterioratingWings : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Deteriorating Wings");
            // Description.SetDefault("Your wings are crumbling into black dust");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
	
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().brokenWings = true;
        }
    }
}