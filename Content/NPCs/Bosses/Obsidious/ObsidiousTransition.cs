using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Obsidious
{
    public class ObsidiousTransition : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 48;
            NPC.aiStyle = -1;
            NPC.lifeMax = 20;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
            Music = MusicLoader.GetMusicSlot("ElementsAwoken/Sounds/Music/ObsidiousTheme");
            NPC.scale = 1f;
            NPC.npcSlots = 1f;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 16;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter > 4)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 15)
            {
                NPC.frame.Y = 0;
            }
        }
        public override bool PreKill()
        {
            return false;
        }
        public override void AI()
        {
            NPC.ai[0]++;
            if (NPC.ai[0] == 150)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().Obsidious4, new Color(188, 58, 49));
            }
            if (NPC.ai[0] == 200)
            {
                NPC.NewNPC(EAU.NPCs(NPC),(int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Obsidious>());
                NPC.active = false;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}