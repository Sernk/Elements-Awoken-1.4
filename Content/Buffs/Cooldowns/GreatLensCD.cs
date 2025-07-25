using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class GreatLensCD : ModBuff
    {
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Great Lens Cooldown");
            // Description.SetDefault("You cannot use the Great Lens");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
	}
}