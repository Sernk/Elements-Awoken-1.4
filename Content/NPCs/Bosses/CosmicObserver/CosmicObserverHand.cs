using ElementsAwoken.Content.Projectiles.NPCProj.CosmicObserver;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.CosmicObserver
{
    public class CosmicObserverHand : ModNPC
    {
        private int projectileBaseDamage = 20;

        public override void SetDefaults()
        {
            NPC.lifeMax = 4500;
            NPC.damage = 30;
            NPC.defense = 25;
            NPC.knockBackResist = 0f;
            NPC.width = 50;
            NPC.height = 54;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 1f;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 60;
            NPC.lifeMax = 6000;
            if (MyWorld.awakenedMode)
            {
                NPC.damage = 120;
                NPC.lifeMax = 12000;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.localAI[1] == 120)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            if (NPC.localAI[1] >= 300 || NPC.localAI[1] < 120)
            {
                NPC.frame.Y = 0;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, Mod.Find<ModGore>("CosmicObserverHand" + i).Type, NPC.scale);
                }
            }
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.5f, 0.5f, 0.5f);
            NPC parent = Main.npc[(int)NPC.ai[1]];
            Player player = Main.player[parent.target];
            if (!parent.active)
            {
                NPC.active = false;
            }
            NPC.Center = parent.oldPos[4] + new Vector2(parent.width / 2, parent.height / 2) + new Vector2(50 * NPC.ai[0], 40);
            NPC.rotation = 0;
            NPC.spriteDirection = (int)NPC.ai[0];

            if (parent.ai[1] < 600 || MyWorld.awakenedMode)
            {
                NPC.localAI[1]++;
                if (NPC.localAI[1] == 120)
                {
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<ObserverSpell>(), projectileBaseDamage, 0, Main.myPlayer, 0, NPC.whoAmI);
                }
                if (NPC.localAI[1] >= 300)
                {
                    NPC.localAI[1] = 0;
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }   
}