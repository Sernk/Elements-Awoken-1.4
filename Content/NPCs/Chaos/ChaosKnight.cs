using ElementsAwoken.Content.Items.Chaos;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Chaos
{
    public class ChaosKnight : ModNPC
	{
		public override void SetDefaults()
		{
			NPC.width = 40;
			NPC.height = 56;
			NPC.damage = 200;
			NPC.defense = 100;
			NPC.lifeMax = 3000;
            NPC.knockBackResist = 0.75f;
            NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath3;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
			NPC.aiStyle = 3;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];
			AIType = NPCID.Skeleton;
			AnimationType = NPCID.PossessedArmor;
            NPC.buffImmune[24] = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Knight");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];
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
	}
}