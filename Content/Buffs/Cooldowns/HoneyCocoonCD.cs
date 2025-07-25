using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class HoneyCocoonCD : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Honey Cocoon Cooldown");
            // Description.SetDefault("You cannot use the Honey Cocoon");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
	}
}