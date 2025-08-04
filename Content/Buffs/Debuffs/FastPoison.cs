using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class FastPoison : ModBuff
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
            npc.GetGlobalNPC<NPCsGLOBAL>().fastPoison = true;
            if (Main.rand.Next(30) == 0)
            {
                int num38 = Dust.NewDust(npc.position, npc.width, npc.height, 60, 0f, 0f, 120, default, 0.2f);
                Main.dust[num38].noGravity = true;
                Main.dust[num38].fadeIn = 1.9f;
            }
        }
    }
}