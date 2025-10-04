using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.VoidStone;
using ElementsAwoken.Content.Items.VoidEventItems;
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
    public class VoidFly : ModNPC
    {
        private float mode
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float parentWhoAmI
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float shootTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float flyTimer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 10;
            NPC.height = 12;
            NPC.aiStyle = -1;
            NPC.damage = 150;
            NPC.defense = 35;
            NPC.lifeMax = 600;
            NPC.knockBackResist = 0.25f;
            NPC.npcSlots = 0.5f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
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
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.VoidFly")]);
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage = NPCsGLOBAL.ReducePierceDamage(modifiers.SourceDamage, projectile);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 900;
            NPC.defense = 50;
            NPC.damage = 200;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 1500;
                NPC.defense = 65;
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
            if (NPC.frame.Y > frameHeight * 3)  // so it doesnt go over
            {
                NPC.frame.Y = 0;
            }
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];

            if (mode == 0)
            {
                if (NPC.localAI[0] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    shootTimer = 90;
                    int numFlies = Main.expertMode ? MyWorld.awakenedMode ? 12 : 8 : 5;
                    for (int l = 0; l < numFlies; l++)
                    {
                        NPC fly = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X + Main.rand.Next(-20, 20), (int)NPC.Center.Y + Main.rand.Next(-30, 30), ModContent.NPCType<VoidFly>(),NPC.whoAmI,1,NPC.whoAmI, 240 + (l * 90))]; 
                    }
                    NPC.localAI[0]++;
                }
                bool anyFlyChildren = false;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC other = Main.npc[i];
                    if (other.type == NPC.type && other.ai[1] == NPC.whoAmI && other.active && other.whoAmI != NPC.whoAmI)
                    {
                        anyFlyChildren = true;
                    }
                }
                if (!anyFlyChildren)
                {
                    shootTimer--;
                    if (shootTimer <= 0)
                    {
                        Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                        toTarget.Normalize();
                        NPC.velocity = toTarget * 10;
                        mode = 2;
                    }
                }
                else
                {
                    Move(P, 0.015f, P.Center - new Vector2(0, 150));
                }
            }
            else if (mode == 1)
            {
                NPC parent = Main.npc[(int)parentWhoAmI];

                if (!parent.active || Collision.CanHit(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height) && Vector2.Distance(NPC.Center, P.Center) < 900)
                {
                    shootTimer--;
                    if (shootTimer <= 0)
                    {
                        Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                        toTarget.Normalize();
                        NPC.velocity = toTarget * 10;
                        mode = 2;
                    }
                }
                Vector2 diff = parent.position - parent.oldPosition;
                NPC.position += diff;
                Move(P, 0.01f, parent.Center);
            }
            else
            {
                flyTimer++;
                int boomHitboxSize = 20;
                if (flyTimer > 90 || (Collision.SolidCollision(NPC.Center - Vector2.One * (boomHitboxSize / 2), boomHitboxSize, boomHitboxSize) || Vector2.Distance(NPC.Center, P.Center) < 20))
                {
                    Explosion(P);
                    NPC.active = false;
                }
            }


            //STOP CLUMPING FOOLS
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
            NPCsGLOBAL.GoThroughPlatforms(NPC);
        }
        private void Explosion(Player player)
        {
            Projectile exp = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ExplosionHostile>(), 120, 5f, player.whoAmI, 0f, 0f)];
            SoundEngine.PlaySound(SoundID.Item14, NPC.position);
            int num = ModContent.GetInstance<Config>().lowDust ? 10 : 20;
            for (int i = 0; i < num; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 31, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 1.4f;
            }
            int num2 = ModContent.GetInstance<Config>().lowDust ? 5 : 10;
            for (int i = 0; i < num2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 127, 0f, 0f, 100, default(Color), 2.5f)];
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust = Main.dust[Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 127, 0f, 0f, 100, default(Color), 1.5f)];
                dust.velocity *= 3f;
            }
            int num373 = Gore.NewGore(EAU.NPCs(NPC), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(EAU.NPCs(NPC), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(EAU.NPCs(NPC), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(EAU.NPCs(NPC), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - NPC.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;
            if (Vector2.Distance(target, NPC.Center) > 300) speed *= 2f;
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
            float slowSpeed = Main.expertMode ? 0.96f : 0.98f;
            if (MyWorld.awakenedMode) slowSpeed = 0.94f;
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