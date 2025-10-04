using ElementsAwoken.Content.Items.Banners.VoidEvent;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.VoidStone;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1
{
    public class Immolator : ModNPC
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
        private float shootTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 20;
            NPC.aiStyle = -1;
            NPC.damage = 150;
            NPC.defense = 35;
            NPC.lifeMax = 1000;
            NPC.knockBackResist = 0.25f;
            NPC.npcSlots = 0.5f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<ImmolatorBanner>();
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
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.Immolator")]);
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage = NPCsGLOBAL.ReducePierceDamage(modifiers.SourceDamage, projectile);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 175;
            NPC.lifeMax = 1500;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 1500;
                NPC.damage = 200;
                NPC.defense = 50;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidStone>(), 2, 3, 5));
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
            if (NPC.frame.Y > frameHeight * 5)  // so it doesnt go over
            {
                NPC.frame.Y = 0;
            }
        }

        public override void AI()
        {
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || P.dead || !P.active || Vector2.Distance(P.Center,NPC.Center) > 2000) NPC.TargetClosest(true);
            changeLocationTimer--;
            if (changeLocationTimer <= 0)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        vectorX = 400f + Main.rand.Next(-100, 100);
                        break;
                    case 1:
                        vectorX = -400f + Main.rand.Next(-100, 100);
                        break;
                    default: break;
                }
                changeLocationTimer = 180;
                NPC.netUpdate = true;
            }
            Move(P, 0.05f, new Vector2(P.Center.X + vectorX, P.Center.Y - 200));           

            Vector2 direction = P.Center - NPC.Center;
            NPC.direction = Math.Sign(direction.X);
            NPC.rotation = direction.ToRotation();
            if (direction.X < 0f) NPC.rotation += 3.14f;

            shootTimer--;
            if (shootTimer <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item12, NPC.position);

                int damage = Main.expertMode ? MyWorld.awakenedMode ? 60 : 40 : 20;
                float Speed = 10f;
                float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                Projectile ball = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<ImmolatorBall>(), damage, 0f, 0)];
                ball.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                shootTimer = 160;
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