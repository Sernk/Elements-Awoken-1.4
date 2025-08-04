using ElementsAwoken.EASystem;
using Terraria;
using Terraria.ModLoader;
namespace ElementsAwoken.Content.Buffs
{
    public class ElementalArmorCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            EAU.CanBeCleared(Type);
        }	
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().elementalArmorCooldown = true;
		}
	}
}