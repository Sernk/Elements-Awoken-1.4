using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs
{
    public class NeovirtuoCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Const.Longer(Type);
            Const.CanBeCleared(Type);
        }
	}
}