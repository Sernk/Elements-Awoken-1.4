using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class Dragonfire : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            EAU.Longer(Type);
        }	
		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<NPCsGLOBAL>().dragonfire = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 127);
            Main.dust[num1].scale = (float)Main.rand.Next(70, 110) * 0.02f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
            if (npc.wet) npc.buffTime[buffIndex] = 0;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().dragonfire = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, 127);
            Main.dust[num1].scale = (float)Main.rand.Next(70, 110) * 0.02f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
            if (player.wet) player.buffTime[buffIndex] = 0;
        }
    }
}