using ElementsAwoken.Content.Items.Banners.VoidEvent;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.VoidStone;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase2
{
    public class VoidCrawler : ModNPC
	{
        public int jumpCooldown = 0;

        public override void SetDefaults()
		{
			NPC.npcSlots = 0.5f;
			NPC.width = 100;
			NPC.height = 50;
            NPC.damage = 160;
            NPC.defense = 65;
			NPC.lifeMax = 1500;
			NPC.knockBackResist = 0.1f;
			AnimationType = 257;
            NPC.aiStyle = 3;
            AIType = NPCID.AnomuraFungus;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit29;
			NPC.DeathSound = SoundID.NPCDeath31;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<VoidCrawlerBanner>();
            SpawnModBiomes = [ModContent.GetInstance<DOTVBiome>().Type];
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.VoidCrawler")]);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 2000;
            NPC.damage = 250;
            NPC.defense = 75; 
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 15000;
                NPC.defense = 85;
                NPC.damage = 350;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Slow, 180, false);
            target.AddBuff(EAU.HandsOfDespair, 180, false);
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.1f, 0.1f, 0.5f);
            Player P = Main.player[NPC.target];
            NPC.TargetClosest(true);

            if (Main.rand.Next(500) == 0)
            {
                SoundEngine.PlaySound(SoundID.NPCHit29, NPC.position);
            }
            // jump up if the player is above and near:
            // x check, must be within 400 pix
            jumpCooldown--;
            float jumpspeed = 10.5f;
            if (Math.Abs(NPC.Center.X - P.Center.X) <= 100)
            {
                if (NPC.Bottom.Y > P.Bottom.Y) // under the player
                {
                    if (NPC.velocity.Y == 0 && jumpCooldown <= 0) // grounded
                    {
                        NPC.velocity.Y -= jumpspeed;
                        jumpCooldown = 15;
                    }
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidStone>(), 2, 3, 5));
        }
    }
}