using ElementsAwoken.Content.Items.Banners.Elementals;
using ElementsAwoken.Content.Items.Essence;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.NPCs.Elementals
{
    public class VoidElemental : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 48;
            NPC.damage = 80;
            NPC.defense = 40;
            NPC.lifeMax = 1100;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 5;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            AIType = NPCID.Wraith;
            AnimationType = NPCID.Wraith;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<VoidElementalBanner>();
            SpawnModBiomes = [ModContent.GetInstance<DOTVBiome>().Type];
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.VoidElemental")]);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.SpawnTileY < Main.rockLayer) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && MyWorld.downedVoidEvent && !Main.dayTime ? 0.04f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 1.0f, 0.3f, 0.0f);

            //STOP CLUMPING FOOLS (dotv event)
            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (k != NPC.whoAmI && other.type == NPC.type && other.active && Math.Abs(NPC.position.X - other.position.X) + Math.Abs(NPC.position.Y - other.position.Y) < NPC.width)
                {
                    const float pushAway = 0.05f;
                    if (NPC.position.X < other.position.X)
                    {
                        NPC.velocity.X -= pushAway;
                    }
                    else
                    {
                        NPC.velocity.X += pushAway;
                    }
                    if (NPC.position.Y < other.position.Y)
                    {
                        NPC.velocity.Y -= pushAway;
                    }
                    else
                    {
                        NPC.velocity.Y += pushAway;
                    }
                }
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Slow, 300, false);
            target.AddBuff(EAU.HandsOfDespair, 180, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot) => npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 3));
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossAdjustment);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
    }
}