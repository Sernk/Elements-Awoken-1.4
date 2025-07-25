﻿using ElementsAwoken.EASystem.Global;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Buffs.Debuffs
{
    public class FastPoison : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Rapid Poison");
            //Description.SetDefault("Rapidly losing life");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
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