using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Wasteland
{
    public class WastelandDeath : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Bosses/Wasteland/Wasteland"; } }
        private float aiTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 140;
            NPC.height = 130;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 10;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath36;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            NPC.velocity.Y = 3f;
            for (int k = 0; k < 10; k++)
            {
                int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 32);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity *= 0.1f;
            }
            if (aiTimer >= 180) NPC.active = false;
        }
    }
}