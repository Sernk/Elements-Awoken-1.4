using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class ChaosShieldCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Chaos Shield Cooldown");
            // Description.SetDefault("You cannot create a chaos shield");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
		}
	}
}