using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class Discord : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Discord");
            // Description.SetDefault("Every part of you aches...");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
		
		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<NPCsGLOBAL>().discordDebuff = true;
            int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 39);
            Main.dust[num1].scale = (float)Main.rand.Next(70, 110) * 0.01f;
            Main.dust[num1].alpha = 100;
            //Main.dust[num1].velocity *= 3f;
            //Main.dust[num1].noGravity = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().discordDebuff = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, 39);
            Main.dust[num1].scale = (float)Main.rand.Next(70, 110) * 0.01f;
            Main.dust[num1].alpha = 100;
            //Main.dust[num1].velocity *= 3f;
            //Main.dust[num1].noGravity = true;
        }
    }
}