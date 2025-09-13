using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.ItemSets.Puff;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Puff
{
	public class Puff : ModNPC
	{
		public override void SetDefaults()
		{
			NPC.aiStyle = 1;
			NPC.damage = 6;
			NPC.width = 32; //324
			NPC.height = 22; //216
			NPC.defense = 2;
			NPC.lifeMax = 13;
			AnimationType = 1;
            NPC.value = Item.buyPrice(0, 0, 1, 0);
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            //banner = npc.type;
            //bannerItem = mod.ItemType("PuffBanner");
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * bossAdjustment);
			NPC.damage = (int)(NPC.damage * 0.5f);
		}	
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode) target.AddBuff(ModContent.BuffType<Cuddled>(), 2000);
            else target.AddBuff(ModContent.BuffType<Cuddled>(), 500);
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot) => npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Puffball>(), 1, 1, 3));
    }
}