using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Ancients
{
    public class ShardBase : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            NPC.lifeMax = 10000;
            NPC.aiStyle = -1;
            NPC.width = 58;
            NPC.height = 72;
            NPC.noGravity = true;
            NPC.netAlways = true;
            NPC.dontTakeDamage = true;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            if (!P.active || P.dead) NPC.TargetClosest(true);
            if (Main.netMode == 0)
            {
                if (!P.active || P.dead) NPC.active = false;
            }
            else
            {
                if (!P.active || P.dead)
                {
                    NPC.TargetClosest(true);
                    if (!P.active || P.dead)
                    {
                        NPC.active = false;
                    }
                }
            }
            NPC.Center = P.Center + new Vector2(0, -300);

            if (!AnyAncients())
            {
                NPC.ai[1]++;
                if (NPC.ai[1] == 180)
                {
                    SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/NPC/AncientMergeRise"), NPC.position);
                }
                if (NPC.ai[1] == 300 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.autoPause == true)
                    {
                        string text = ModContent.GetInstance<EALocalization>().ShardBase;
                        if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(text, Color.LightCyan);
                        else ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.LightCyan);
                    }
                    NPC aa = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AncientAmalgam>())];
                    aa.netUpdate = true;
                    NPC.active = false;
                }
            }
        }
        private bool AnyAncients()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Izaris>())) return true;
            if (NPC.AnyNPCs(ModContent.NPCType<Kirvein>())) return true;
            if (NPC.AnyNPCs(ModContent.NPCType<Krecheus>())) return true;
            if (NPC.AnyNPCs(ModContent.NPCType<Xernon>())) return true;
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}
