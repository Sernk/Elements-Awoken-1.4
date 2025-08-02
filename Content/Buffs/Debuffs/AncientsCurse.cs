using ElementsAwoken.Content.Dusts.Ancients;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class AncientsCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
        }
		public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<AncientGreen>())];
            dust.scale = Main.rand.NextFloat(0.5f,1.1f);
            dust.velocity *= 0.5f;
            dust.noGravity = true;
            npc.defense = (int)(npc.defense * 0.5f);
        }
    }
}