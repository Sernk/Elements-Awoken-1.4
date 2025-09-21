using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.ItemSets.Radia;
using ElementsAwoken.Content.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Events.RadiantRain.Enemies
{
    public class StellarStarfish : ModNPC
    {
        private float timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 26;
            NPC.aiStyle = 44;
            NPC.lifeMax = 7600;
            NPC.damage = 100;
            NPC.defense = 40;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.value = Item.buyPrice(0, 3, 0, 0);
            NPC.knockBackResist = 0.5f;
            SpawnModBiomes = [ModContent.GetInstance<RadiantRainBiome>().Type];
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.StellarStarfish")]);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 15000;
            NPC.damage = 150;
            NPC.defense = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 20000;
                NPC.damage = 200;
                NPC.defense = 65;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Radia>()));
        }
        public override void AI()
        {
            FallThroughPlatforms();
            Player P = Main.player[NPC.target];
            NPC.rotation += NPC.velocity.X * 0.02f;
            timer--;
            if (Math.Abs(P.Center.X - NPC.Center.X) < 60 && timer <= 0 && P.Center.Y > NPC.Center.Y && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int n = 0; n < 3; n++)
                {
                    float speed = 2f;
                    Vector2 vector4 = new Vector2(0, speed).RotatedByRandom(MathHelper.ToRadians(50));
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, vector4.X, vector4.Y, ProjectileType<RadiantStar>(), NPC.damage / 3, 0f, Main.myPlayer, 0f, 0f);
                    timer = 60f;
                }
            }
            if (Math.Abs(P.Center.X - NPC.Center.X) < 120 && Math.Abs(P.Center.Y - NPC.Center.Y) < 300)  NPC.velocity.Y -= 0.04f;
            if (!GetInstance<Config>().lowDust)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, EAU.PinkFlame)];
                    dust.noGravity = true;
                    dust.velocity *= 0.1f;
                    dust.fadeIn = 0.8f;
                    dust.scale *= 0.2f;
                }
            }
        }
      
        private void FallThroughPlatforms()
        {
            Player P = Main.player[NPC.target];
            Vector2 platform = NPC.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.TileType] && NPC.Bottom.Y < P.Bottom.Y && platformTile.HasTile) NPC.position.Y += 0.3f;
        }
    }
}
