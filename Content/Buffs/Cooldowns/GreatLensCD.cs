﻿using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Cooldowns
{
    public class GreatLensCD : ModBuff
    {
        public override void SetStaticDefaults()
		{
            Const.CanBeCleared(Type);
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
	}
}