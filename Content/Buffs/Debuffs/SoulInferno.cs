using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class SoulInferno : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
        }
		public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().soulInferno = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 173);  
            Main.dust[num1].scale = 1f;
            Main.dust[num1].velocity *= 0.5f;
            Main.dust[num1].noGravity = true;
        }
    }
}