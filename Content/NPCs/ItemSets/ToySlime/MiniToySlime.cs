using ElementsAwoken.Content.Items.ItemSets.ToySlime;
using ElementsAwoken.Content.Projectiles.NPCProj.ToySlime;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.ItemSets.ToySlime
{
    public class MiniToySlime : ModNPC
	{
        public bool dropBlocks = true;
        private float jumpTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float jumpNum
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float shootTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        public override void SetDefaults()
		{
            NPC.width = 32;
            NPC.height = 22;
            NPC.aiStyle = -1;
            NPC.damage = 25;
			NPC.lifeMax = 100;
            NPC.defense = 6;
            NPC.knockBackResist = 0.9f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            NPC.alpha = 75;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mini Toy Slime");
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
            NPC.damage = 45;
            NPC.lifeMax = 200;
            NPC.defense = 12;
            if (MyWorld.awakenedMode)
            {
                NPC.damage = 70;
                NPC.lifeMax = 400;
                NPC.defense = 18;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (jumpTimer <= 40) NPC.frameCounter++;
            if (NPC.frameCounter > 8)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 1)
            {
                NPC.frame.Y = 0;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnchance = 0.1f;
            MyPlayer modPlayer = spawnInfo.Player.GetModPlayer<MyPlayer>();
            if (modPlayer.toySlimeChanceTimer > 0 && !NPC.AnyNPCs(NPCType<ToySlime>()))
            {
                spawnchance = 0.4f;
            }
            //SpawnCondition.OverworldDaySlime.Chance * 0.9f;

            return (spawnInfo.SpawnTileY < Main.worldSurface) && NPC.downedBoss3 && !spawnInfo.PlayerInTown && !spawnInfo.Invasion ? spawnchance : 0f;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.MiniToySlime"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            ]);
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            SlimeAI();
            if (MyWorld.awakenedMode)
            {
                shootTimer--;
                if (shootTimer <= 0)
                {
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1), ProjectileType<LegoBrick>(), NPC.damage, 0f, Main.myPlayer, 0, 0);
                    shootTimer = Main.rand.Next(300, 600);
                }
            }
        }
        public override void OnKill()
        {
            int gelCount = Main.expertMode ? MyWorld.awakenedMode ? Main.rand.Next(2, 5) : Main.rand.Next(1, 4) : Main.rand.Next(1, 3);
            int item = Item.NewItem(EAU.NPCs(NPC), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Gel, gelCount);
            Main.item[item].color = new Color(0, 220, 40, 100);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BrokenToys>(), 1, 1, 3));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = NPC.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = NPC.frame.Size() * 0.5f;
            Color color = drawColor;
            spriteBatch.Draw(Request<Texture2D>("ElementsAwoken/Content/NPCs/ItemSets/ToySlime/MiniToySlimeToys").Value, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color, NPC.rotation, origin, NPC.scale, spriteEffects, 0);
            return true;
        }
        private void SlimeAI()
        {
            NPC.TargetClosest(true);
            if (NPC.wet)
            {
                if (NPC.collideY)
                {
                    NPC.velocity.Y = -2f;
                }
                if (NPC.velocity.Y < 0f && NPC.ai[3] == NPC.position.X)
                {
                    NPC.direction *= -1;
                }
                if (NPC.velocity.Y > 0f)
                {
                    NPC.ai[3] = NPC.position.X;
                }

                if (NPC.velocity.Y > 2f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
                NPC.velocity.Y = NPC.velocity.Y - 0.5f;
                if (NPC.velocity.Y < -4f)
                {
                    NPC.velocity.Y = -4f;
                }
            }
            if (NPC.velocity.Y == 0f)
            {
                if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.position.X = NPC.position.X - (NPC.velocity.X + (float)NPC.direction);
                }
                if (NPC.ai[3] == NPC.position.X)
                {
                    NPC.direction *= -1;
                }
                NPC.ai[3] = 0f;
                NPC.velocity.X = NPC.velocity.X * 0.8f;
                if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                {
                    NPC.velocity.X = 0f;
                }
                jumpTimer--; // jump speed

                if (jumpTimer <= 0)
                {
                    jumpNum++;
                    NPC.netUpdate = true;
                    if (jumpNum == 4)
                    {
                        NPC.velocity.Y = -8f;
                        NPC.velocity.X = NPC.velocity.X + (float)(3 * NPC.direction);
                        jumpTimer = 120f;
                        NPC.ai[3] = NPC.position.X;
                        jumpNum = 0;
                    }
                    else
                    {
                        NPC.velocity.Y = -6f;
                        NPC.velocity.X = NPC.velocity.X + (float)(2 * NPC.direction);
                        jumpTimer = 70f;
                    }
                }
                else if (jumpTimer >= -30f)
                {
                    return;
                }
            }
            else if (NPC.target < 255 && ((NPC.direction == 1 && NPC.velocity.X < 3f) || (NPC.direction == -1 && NPC.velocity.X > -3f)))
            {
                if (NPC.collideX && Math.Abs(NPC.velocity.X) == 0.2f)
                {
                    NPC.position.X = NPC.position.X - 1.4f * (float)NPC.direction;
                }
                if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.position.X = NPC.position.X - (NPC.velocity.X + (float)NPC.direction);
                }
                if ((NPC.direction == -1 && (double)NPC.velocity.X < 0.01) || (NPC.direction == 1 && (double)NPC.velocity.X > -0.01))
                {
                    NPC.velocity.X = NPC.velocity.X + 0.2f * (float)NPC.direction;
                    return;
                }
                NPC.velocity.X = NPC.velocity.X * 0.93f;
            }
        }
    }
}