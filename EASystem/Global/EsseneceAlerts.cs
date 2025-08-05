using ElementsAwoken.EASystem.UI.UIIIII;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Global
{
    public class EsseneceAlerts : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if(npc.type == NPCID.EyeofCthulhu)
            {
                if (!NPC.downedBoss1)
                {
                    UISystemSettings.Panel = true;
                }
            }
        }
    }
}