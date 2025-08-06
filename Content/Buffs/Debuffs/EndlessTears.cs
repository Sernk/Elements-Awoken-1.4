using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class EndlessTears : ModBuff
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
            npc.GetGlobalNPC<NPCsGLOBAL>().endlessTears = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.BlueCrystalShard);
            Main.dust[num1].scale = 0.5f;
            Main.dust[num1].velocity *= 0f;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().endlessTears = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, DustID.BlueCrystalShard);
            Main.dust[num1].scale = 0.5f;
            Main.dust[num1].velocity *= 0f;
        }
    }
}