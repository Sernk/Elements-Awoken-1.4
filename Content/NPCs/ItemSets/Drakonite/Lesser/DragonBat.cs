using ElementsAwoken.Content.Items.Banners;
using ElementsAwoken.Content.Projectiles.NPCProj;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Drakonite.Lesser
{
    public class DragonBat : ModNPC
    {
        public float shootTimer = 180f;

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 20;           
            NPC.aiStyle = 14;
            AIType = NPCID.CaveBat;
            AnimationType = 93;
            NPC.damage = 19;
            NPC.defense = 5;
            NPC.lifeMax = 34;
            NPC.knockBackResist = 0.7f;
            NPC.value = Item.buyPrice(0, 0, 2, 0);
            NPC.npcSlots = 0.5f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath4;
            NPC.buffImmune[24] = true;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DragonBatBanner>();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.DragonBat"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool underworld = (spawnInfo.SpawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.SpawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneDungeon && !Main.hardMode ? 0.06f : 0f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.ItemSets.Drakonite.Regular.Drakonite>(), minimumDropped: 1, maximumDropped: 3));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode) target.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 150 : 90, false);
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            shootTimer -= 1f;
            if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer == 0f && Collision.CanHit(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height))
            {
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                float Speed = 6f;
                int damage = 6;
                float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<DragonBatFireball>(), damage, 0f, 0);
                shootTimer = 120f;
            }          
        }
    }
}