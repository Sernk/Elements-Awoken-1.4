using ElementsAwoken.Content.Items.Chaos;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Chaos
{
    public class ChaosWraith : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 48;
            NPC.damage = 80;
            NPC.defense = 20;
            NPC.lifeMax = 2000;
            NPC.knockBackResist = 0.5f;
            NPC.value = Item.buyPrice(0, 2, 0, 0);
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.aiStyle = 5;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            AIType = NPCID.Wraith;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Wraith");
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (NPC.frameCounter > 5)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y >= frameHeight * 5)  // so it doesnt go over
            {
                NPC.frame.Y = 0;
            }
            if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = 1;
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
            }
            NPC.rotation = NPC.velocity.X * 0.1f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.SpawnTileY < Main.rockLayer) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && !Main.dayTime && MyWorld.downedVoidLeviathan ? 0.065f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 1.0f, 0.1f, 0.1f);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Slow, 300, false);
            target.AddBuff(BuffID.BrokenArmor, 300, false);
            target.AddBuff(BuffID.WitheredArmor, 300, false);
            target.AddBuff(BuffID.WitheredWeapon, 300, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaoticFlare>(), 2, 3, 6));
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossAdjustment);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
    }
}