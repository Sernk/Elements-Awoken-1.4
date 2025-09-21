using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1
{
    public class AbyssSkullette : ModNPC
    {
        private float rotation
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float parentWhoAmI
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
            NPC.width = 14;
            NPC.height = 14;
            NPC.damage = 50;
            NPC.defense = 10;
            NPC.lifeMax = 200;
            NPC.knockBackResist = 0.25f;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
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
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.AbyssSkullette")]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 300;
            NPC.defense = 14;
            NPC.damage = 100;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 700;
                NPC.defense = 40;
                NPC.damage = 200;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage = NPCsGLOBAL.ReducePierceDamage(modifiers.SourceDamage, projectile);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int l = 0; l < 6; l++)
                {
                    Dust dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 54)];
                    dust.velocity.X = hit.HitDirection * 2;
                    dust.color = new Color(120, 120, 255);
                }
            }
        }
        public override void AI()
        {
            NPC.TargetClosest(true);

            Player P = Main.player[NPC.target];

            if (NPC.ai[1] != 0)
            {
                NPC parent = Main.npc[(int)NPC.ai[1] - 1];

                if (parent.active)
                {
                    NPC.ai[0] += 1.5f; // speed
                    float distance = parent.width * 1.1f;
                    double rad = NPC.ai[0] * (Math.PI / 180); // angle to radians
                    NPC.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - NPC.width / 2;
                    NPC.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - NPC.height / 2;

                    NPC.spriteDirection = parent.spriteDirection;
                    if (MyWorld.awakenedMode)
                    {
                        NPC.immortal = true;
                        NPC.dontTakeDamage = true;
                    }
                }
                else
                {
                    NPC.ai[1] = 0;
                }
            }
            else
            {
                NPC.immortal = false;
                NPC.dontTakeDamage = false;
                NPC.noTileCollide = false;

                NPC.spriteDirection = Math.Sign(NPC.velocity.X);
                NPC.rotation = NPC.velocity.X * 0.2f;
                Move(P, 0.15f, P.Center);

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
                NPC.velocity.Y = NPC.velocity.Y + speed * 0.3f;
                if (NPC.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed * 0.3f;
                    return;
                }
            }
            else if (NPC.velocity.Y > desiredVelocity.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed * 0.3f;
                if (NPC.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed * 0.3f;
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