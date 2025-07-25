using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementsAwoken.EASystem;
namespace ElementsAwoken.Content.Buffs
{
    public class ElementalArmorCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Elemental Revive Cooldown");
            // Description.SetDefault("Your revive is recharging");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().elementalArmorCooldown = true;
		}
	}
}