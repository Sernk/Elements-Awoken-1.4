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

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1
{
    public class AbyssSkull : ModNPC
    {
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
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.noGravity = true;
            NPC.buffImmune[24] = true;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            SpawnModBiomes = [ModContent.GetInstance<DOTVBiome>().Type];
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
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.AbyssSkull")]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 2000;
            NPC.defense = 50;
            NPC.damage = 200;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 3000;
                NPC.defense = 65;
                NPC.damage = 300;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidStone>(), 2, 3, 5));
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage = NPCsGLOBAL.ReducePierceDamage(modifiers.SourceDamage, projectile);
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            if (Vector2.Distance(P.Center, NPC.Center) < 400) aiState = 1;
            else aiState = 0;

            NPC.spriteDirection = Math.Sign(NPC.velocity.X);
   
            if (aiState == 0)
            {
                changeLocationTimer--;
                if ((vectorX == 0 || vectorY == 0) || changeLocationTimer <= 0 || MathHelper.Distance(vectorX,P.Center.X) > 2000)
                {
                    float midX = (P.Center.X + NPC.Center.X) / 2;
                    vectorX = midX + Main.rand.Next(-200,200);
                    if (Main.rand.Next(3) == 0) vectorX = NPC.Center.X + Main.rand.Next(-200, 200);
                    vectorY = P.Center.Y + Main.rand.Next(-100,100);
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
            }
            else
            {
                Dust dust = Main.dust[Dust.NewDust(NPC.Center - new Vector2(0, -2) - Vector2.One * 4, 2, 2, 127)];
                dust.velocity = Vector2.Zero;
                dust.noGravity = true;
                dust.scale = 1f;

                Move(P, 0.05f, P.Center);
            }

            if (NPC.localAI[0] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int numSkullettes = Main.expertMode ? MyWorld.awakenedMode ? 4 : 3 : 2;
                for (int l = 0; l < numSkullettes; l++)
                {
                    int distance = 360 / numSkullettes;
                    NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AbyssSkullette>(), NPC.whoAmI, l * distance, NPC.whoAmI + 1); //add one so that if no parent then we can check for ai[1] == 0 without risk of the parent being 0
                }
                NPC.localAI[0]++;
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