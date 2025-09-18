using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class ElectrifiedNPC : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().electrified = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 226);
            Main.dust[num1].scale = 1f;
            Main.dust[num1].velocity *= 1f;
            Main.dust[num1].noGravity = true;
        }
    }
}