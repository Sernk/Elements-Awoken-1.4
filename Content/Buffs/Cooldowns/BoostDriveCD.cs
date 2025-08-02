using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class BoostDriveCD : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Const.CanBeCleared(Type);
        }
	}
}