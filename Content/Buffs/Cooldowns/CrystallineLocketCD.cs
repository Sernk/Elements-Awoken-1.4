using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class CrystallineLocketCD : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Crystalline Locket Cooldown");
            // Description.SetDefault("You cannot use the crystalline locket");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
	}
}