﻿using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class ArchaicProtectionCD : ModBuff
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