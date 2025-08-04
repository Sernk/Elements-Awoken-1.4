using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class HoneyCocoonCD : ModBuff
	{
		public override void SetStaticDefaults()
		{
            EAU.CanBeCleared(Type);
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
	}
}