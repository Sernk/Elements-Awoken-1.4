using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.VoidStone;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1
{
    public class AccursedFlier : ModNPC
    {
        private float aiTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(aiTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aiTimer = reader.ReadSingle();
        }
        private float changeLocationTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float vectorX
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float vectorY
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float aiState
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 26;
            NPC.aiStyle = -1;
            NPC.damage = 150;
            NPC.defense = 35;
            NPC.lifeMax = 1000;
            NPC.knockBackResist = 0.25f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath30;
            NPC.noGravity = true;
            NPC.buffImmune[24] = true;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            SpawnModBiomes = [ModContent.GetInstance<DOTVBiome>().Type];
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage = NPCsGLOBAL.ReducePierceDamage(modifiers.SourceDamage,projectile);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.AccursedFlier")]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 2000;
            NPC.defense = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 3000;
                NPC.defense = 65;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter > 6)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 3)  // so it doesnt go over
            {
                NPC.frame.Y = 0;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidStone>(), 2, 3, 5));
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];

            if (aiState == 0)
            {
                changeLocationTimer--;
                if ((vectorX == 0 || vectorY == 0) || changeLocationTimer <= 0 || MathHelper.Distance(vectorX, P.Center.X) > 2000 || Vector2.Distance(NPC.Center, new Vector2(vectorX, vectorY)) < 30)
                {
                    float midX = (P.Center.X + NPC.Center.X) / 2;
                    vectorX = midX + Main.rand.Next(-200, 200);
                    if (Main.rand.Next(6) == 0) vectorX = NPC.Center.X + Main.rand.Next(-200, 200);
                    vectorY = P.Center.Y + Main.rand.Next(-100, 100);
                    changeLocationTimer = 190;
                    NPC.netUpdate = true;
                }
                Vector2 targetLoc = new Vector2(vectorX, vectorY);
                Move(P, 0.015f, targetLoc);
                if (ModContent.GetInstance<Config>().debugMode)
                {
                    Dust dust = Main.dust[Dust.NewDust(targetLoc, 2, 2, 6)];
                    dust.noGravity = true;
                }

                aiTimer++;
                if (aiTimer > 240 && Vector2.Distance(P.Center, NPC.Center) < 400)
                {
                    aiState = 1;
                    aiTimer = 0;
                }
                NPC.direction = Math.Sign(NPC.velocity.X);
            }
            else if (aiState == 1)
            {
                Vector2 targetLoc = new Vector2(P.Center.X, P.Center.Y - 120);
                Vector2 toTarget = new Vector2(targetLoc.X - NPC.Center.X, targetLoc.Y - NPC.Center.Y);
                toTarget.Normalize();
                NPC.velocity = toTarget * 4;
                if (Vector2.Distance(targetLoc, NPC.Center) < 30)
                {
                    aiState = 2;
                }
                NPC.direction = Math.Sign(NPC.velocity.X);
            }
            else if (aiState == 2)
            {
                NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);

                NPC.velocity *= 0.96f;
                aiTimer++;
                if (aiTimer % 6 == 0)
                {
                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<AccursedBreath>(), 30, 0f, 0);
                }
                if (aiTimer % 20==0) SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);
                if (aiTimer >= 90)
                {
                    aiState = 3;
                    aiTimer = 0;
                }
            }
            else
            {
                aiTimer++;
                Vector2 targetLoc = new Vector2(P.Center.X, P.Center.Y - 120);
                Vector2 toTarget = new Vector2(targetLoc.X - NPC.Center.X, targetLoc.Y - NPC.Center.Y);
                toTarget.Normalize();
                NPC.velocity.X = -toTarget.X * 10;
                NPC.velocity.Y = -toTarget.Y * 1;
                if (aiTimer > 20)
                {
                    aiState = 0;
                    aiTimer = 0;
                }
                NPC.direction = Math.Sign(NPC.velocity.X);
            }

            NPCsGLOBAL.GoThroughPlatforms(NPC);
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - NPC.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;

            if (NPC.velocity.X < desiredVelocity.X)
            {
                NPC.velocity.X = NPC.velocity.X + speed;
                if (NPC.velocity.X < 0f && desiredVelocity.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + speed;
                }
            }
            else if (NPC.velocity.X > desiredVelocity.X)
            {
                NPC.velocity.X = NPC.velocity.X - speed;
                if (NPC.velocity.X > 0f && desiredVelocity.X < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - speed;
                }
            }
            if (NPC.velocity.Y < desiredVelocity.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y + speed * 0.5f;
                if (NPC.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed * 0.5f;
                    return;
                }
            }
            else if (NPC.velocity.Y > desiredVelocity.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed * 0.5f;
                if (NPC.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed * 0.5f;
                    return;
                }
            }
            float slowSpeed = Main.expertMode ? 0.97f : 0.99f;
            if (MyWorld.awakenedMode) slowSpeed = 0.96f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((NPC.velocity.X < xSign && xSign == 1) || (NPC.velocity.X > xSign && xSign == -1)) NPC.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, NPC.Center.Y) > 1000)
            {
                if ((NPC.velocity.X < ySign && ySign == 1) || (NPC.velocity.X > ySign && ySign == -1)) NPC.velocity.Y *= slowSpeed;
            }
        }
    }
}