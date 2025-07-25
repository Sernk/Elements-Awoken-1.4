using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class BoostDriveCD : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Boost Drive Cooldown");
            // Description.SetDefault("You cannot use the Boost Drive");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
	}
}