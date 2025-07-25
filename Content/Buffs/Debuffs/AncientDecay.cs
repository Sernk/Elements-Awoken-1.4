using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class AncientDecay : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Ancient Decay");
            // Description.SetDefault("Your soul is wearing away");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			BuffID.Sets.LongerExpertDebuff[Type] = true;
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<NPCsGLOBAL>().ancientDecay = true;
		}
	}
}