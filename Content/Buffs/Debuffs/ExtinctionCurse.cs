using ElementsAwoken.EASystem;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class ExtinctionCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Extinction Curse");
            // Description.SetDefault("The forces of the abyss pull you deeper...");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCsGLOBAL>().extinctionCurse = true;
            if (ModContent.GetInstance<Config>().lowDust)
            {
                Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.Firework_Pink)];
                dust.scale = 0.7f;
                dust.fadeIn = 1f;
                dust.noGravity = true;
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MyPlayer>().extinctionCurse = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, DustID.Firework_Pink);
            Main.dust[num1].scale = 2.9f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = true;
        }

    }
}