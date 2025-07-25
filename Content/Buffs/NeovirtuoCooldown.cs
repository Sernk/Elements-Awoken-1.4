using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class NeovirtuoCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Neovirtuo Cooldown");
            // Description.SetDefault("You cant use Neovirtuo");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
	}
}