using ElementsAwoken.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Critters 
{
    public class MysticBunny : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 20;
            NPC.damage = 0;
            NPC.defense = 11;
            NPC.lifeMax = 20;
            AnimationType = NPCID.Bunny;
            NPC.aiStyle = 7;
            AIType = NPCID.Bunny;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] 
            {
                new FlavorTextBestiaryInfoElement(EALocalization.MysticBunny),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneHallow) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            spawnInfo.SpawnTileY < Main.rockLayer &&
            !Main.snowMoon && 
            !Main.pumpkinMoon && 
            Main.dayTime 
            ? 0.3f : 0f;
        }
    }
}