using ElementsAwoken.Content.Items.Banners.VoidEvent;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.VoidStone;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase2
{
    public class VoidGolem : ModNPC
	{
        public int jumpCooldown = 0;
		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 100;
			NPC.defense = 100;
			NPC.lifeMax = 10000;
            NPC.knockBackResist = 0.05f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath8;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            AIType = NPCID.Skeleton;
            NPC.aiStyle = 3;
            NPC.buffImmune[24] = true;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<VoidGolemBanner>();
            SpawnModBiomes = [ModContent.GetInstance<DOTVBiome>().Type];
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.VoidGolem")]);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            Main.npcFrameCount[NPC.type] = 7;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 11000;
            NPC.damage = 200;
            NPC.defense = 120; 
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 12500;
                NPC.defense = 140;
                NPC.damage = 400;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Slow, 180, false);
            target.AddBuff(EAU.HandsOfDespair, 180, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidStone>(), 2, 3, 5));
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.1f, 0.1f, 0.5f);
            Player P = Main.player[NPC.target];
            NPC.TargetClosest(true);
            // jump up if the player is above and near:
            // x check, must be within 400 pix
            jumpCooldown--;
            float jumpspeed = 9.5f;
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
            if (Main.netMode != NetmodeID.Server)
            {
                Player player = Main.LocalPlayer;
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                if (Vector2.Distance(player.Center, NPC.Center) <= 200) modPlayer.screenshakeAmount = MathHelper.Lerp(4, 0, MathHelper.Clamp(Vector2.Distance(NPC.Center, player.Center) / 200, 0, 1));
            }
        }
        public override void FindFrame(int frameHeight)
        {
            //npc.frameCounter += 1;
            // moving
            if (NPC.velocity.X != 0f)
            {
                NPC.frameCounter += (double)Math.Abs(NPC.velocity.X);
            }

            // on the ground 
            if (NPC.velocity.Y == 0f)
            {
                if (NPC.direction == 1)
                {
                    NPC.spriteDirection = 1;
                }
                if (NPC.direction == -1)
                {
                    NPC.spriteDirection = -1;
                }
            }
            // moving  not jumping 
            if (NPC.velocity.Y == 0f || NPC.velocity.X != 0f)//(npc.direction == -1 && npc.velocity.X > 0f) || (npc.direction == 1 && npc.velocity.X < 0f))
            {
                if (NPC.frameCounter > 8)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 6)  // so it doesnt go over
                {
                    NPC.frame.Y = frameHeight * 1;
                }
            }
            // jumping
            if (NPC.velocity.Y != 0f)
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = 0;
            }
        }
    }
}