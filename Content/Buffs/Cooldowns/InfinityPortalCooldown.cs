using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class InfinityPortalCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			EAU.CanBeCleared(Type);
        }
	}
}