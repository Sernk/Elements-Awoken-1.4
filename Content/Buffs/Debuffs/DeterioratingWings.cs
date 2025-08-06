using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class DeterioratingWings : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            EAU.Longer(Type);
            EAU.CanBeCleared(Type);
        }	
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().brokenWings = true;
        }
    }
}