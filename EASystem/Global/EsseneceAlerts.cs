using ElementsAwoken.EASystem.UI.UIIIII;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ElementsAwoken.EASystem.Global
{
    public class EsseneceAlerts : GlobalNPC, ILocalizedModType
    {
        public string LocalizationCategory => "EsseneceAlertsLocalization";

        public override void Load()
        {
            _ = this.GetLocalization("OnKill.Boss").Value;
            _ = this.GetLocalization("OnKill.Boss1").Value;
            _ = this.GetLocalization("OnKill.Boss2").Value;
            _ = this.GetLocalization("OnKill.Boss3").Value;
            _ = this.GetLocalization("OnKill.Boss4").Value;
            _ = this.GetLocalization("OnKill.Boss5").Value;
        }
        public override void OnKill(NPC npc)
        {
            if(npc.type == NPCID.EyeofCthulhu)
            {
                if (!NPC.downedBoss1)
                {
                    UISystemSettings.Panel = true;
                    string text = this.GetLocalization("OnKill.Boss").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Yellow);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Yellow);
                }
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                if (!NPC.downedBoss3)
                {
                    string text = this.GetLocalization("OnKill.Boss1").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Orange);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Orange);
                }
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && !NPC.downedMechBoss3)
                {
                    string text = this.GetLocalization("OnKill.Boss2").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Cyan);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Cyan);
                }
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss3 && !NPC.downedMechBoss2)
                {
                    string text = this.GetLocalization("OnKill.Boss2").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Cyan);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Cyan);
                }
            }
            if (npc.type == NPCID.Spazmatism && NPC.AnyNPCs(NPCID.Retinazer))
            {
                if (NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.downedMechBoss1)
                {
                    string text = this.GetLocalization("OnKill.Boss2").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Cyan);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Cyan);
                }
            }
            if (npc.type == NPCID.Retinazer && NPC.AnyNPCs(NPCID.Spazmatism))
            {
                if (NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    string text = this.GetLocalization("OnKill.Boss2").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Cyan);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Cyan);
                }
            }
            if (npc.type == NPCID.Plantera)
            {
                if (!NPC.downedPlantBoss)
                {
                    string text = this.GetLocalization("OnKill.Boss3").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.LightBlue);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.LightBlue);
                }
            }
            if (npc.type == NPCID.DukeFishron)
            {
                if (!NPC.downedFishron)
                {
                    string text = this.GetLocalization("OnKill.Boss4").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.MediumBlue);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.MediumBlue);
                }
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                if (!NPC.downedMoonlord)
                {
                    string text = this.GetLocalization("OnKill.Boss5").Value;
                    if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.Red);
                    else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Red);
                }
            }
        }
    }
}