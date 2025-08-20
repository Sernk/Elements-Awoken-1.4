using ElementsAwoken.Content.Projectiles.NPCProj.Ancients;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Ancients
{
    public class AncientAmalgamFist : ModNPC
    {
        float handSwipeTimer = 0;
        bool hasOverlay = false;
        float despawnDelay = 2;

        public int spinAI = 0;
        public int spinTimer = 0;
        public int spinProjTimer = 0;
        public Vector2 spinOrigin = new Vector2();
        public int spinDetectDelay = 0;
        public bool spinDontProj = false;

        public float[] attackAI = new float[4];

        public override void SetDefaults()
        {
            NPC.lifeMax = 10000;
            NPC.damage = 90;
            NPC.defense = 25;
            NPC.knockBackResist = 0f;
            NPC.width = 58;
            NPC.height = 72;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.immortal = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
            NPC.dontTakeDamage = true;
            NPC.npcSlots = 1f;
            NPC.scale *= 1.3f;
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 140;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.ai[2] > 100)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.5f, 0.5f, 0.5f);
            NPC parent = Main.npc[(int)NPC.ai[1]];
            Player player = Main.player[NPC.target];

            List<int> noSwingHands = new List<int>();
            noSwingHands.Add(2);
            noSwingHands.Add(5);
            noSwingHands.Add(6);
            noSwingHands.Add(8);
            bool dontSwing = false;
            foreach (int k in noSwingHands)
            {
                if (parent.ai[2] == k)
                {
                    dontSwing = true;
                }
            }
            NPC.ai[2] = parent.alpha;
            if (!parent.active)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC newParent = Main.npc[i];
                    if (newParent.type == ModContent.NPCType<AncientAmalgamDeath>() && newParent.ai[1] == NPC.ai[1])
                    {
                        NPC.ai[1] = i;
                    }
                }

                despawnDelay--;
                if (despawnDelay <= 0)
                {
                    NPC.active = false;
                }
            }
        
            else
            {
                despawnDelay = 2;
            }
            if (!hasOverlay)
            {
                // bad way to do this probably :lul:
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.position.X, NPC.position.Y, 0, 0, ModContent.ProjectileType<AAHandOverlay>(), 0, 0, Main.myPlayer, 0, NPC.whoAmI);
                NPC.alpha = 255; // so u cant see the weird ass offset :shruggy:
                hasOverlay = true;
            }
            if (parent.type == ModContent.NPCType<AncientAmalgamDeath>())
            {
                attackAI[2] = 0; // stop swiping
                NPC.damage = 0;
            }
            if (dontSwing)
            {
                attackAI[2] = 0; // stop swiping
            }
            if (parent.ai[2] != 2)
            {
                if (attackAI[2] == 0)
                {
                    NPC.ai[3]++;
                    if (NPC.ai[3] >= 300)
                    {
                        NPC.ai[3] = 0;
                        attackAI[2]++;
                    }
                    int fistX1 = 90;
                    if (parent.direction == 1)
                    {
                        fistX1 = 105;
                    }
                    int fistX2 = 105;
                    if (parent.direction == 1)
                    {
                        fistX2 = 90;
                    }
                    int fistPos = NPC.ai[0] == 1 ? fistX1 : -fistX2;
                    float targetX = parent.Center.X + fistPos;
                    float targetY = parent.Center.Y + 140 - (NPC.height * 0.5f);

                    if (Vector2.Distance(new Vector2(targetX, targetY), NPC.Center) >= 40)
                    {
                        NPC.rotation = -((float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f);
                    }
                    else
                    {
                        NPC.rotation = 0;
                    }

                    int maxDist = 500;
                    if (Vector2.Distance(new Vector2(targetX, targetY), NPC.Center) >= maxDist)
                    {
                        float moveSpeed = 18f;
                        Vector2 toTarget = new Vector2(targetX, targetY) - NPC.Center;
                        toTarget.Normalize();
                        NPC.velocity = toTarget * moveSpeed;
                    }
                    else
                    {
                        float speed = 1f;
                        if (NPC.Center.Y > targetY)
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y * 0.96f;
                            }
                            NPC.velocity.Y = NPC.velocity.Y - speed;
                            if (NPC.velocity.Y > 3f)
                            {
                                NPC.velocity.Y = 3f;
                            }
                        }
                        else if (NPC.Center.Y < targetY)
                        {
                            if (NPC.velocity.Y < 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y * 0.96f;
                            }
                            NPC.velocity.Y = NPC.velocity.Y + speed;
                            if (NPC.velocity.Y < -3f)
                            {
                                NPC.velocity.Y = -3f;
                            }
                        }
                        if (NPC.Center.X > targetX)
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X * 0.96f;
                            }
                            NPC.velocity.X = NPC.velocity.X - speed;
                            if (NPC.velocity.X > 12f)
                            {
                                NPC.velocity.X = 12f;
                            }
                        }
                        else if (NPC.Center.X < targetX)
                        {
                            if (NPC.velocity.X < 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X * 0.96f;
                            }
                            NPC.velocity.X = NPC.velocity.X + speed;
                            if (NPC.velocity.X < -12f)
                            {
                                NPC.velocity.X = -12f;
                            }
                        }
                    }
                    NPC.spriteDirection = (int)NPC.ai[0];
                }
                if (attackAI[2] == 1) // move away
                {
                    handSwipeTimer++;

                    float speed = 8f;
                    float num25 = player.Center.X - NPC.Center.X;
                    float num26 = player.Center.Y - NPC.Center.Y;
                    float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26);
                    num27 = speed / num27;
                    NPC.velocity.X = -(num25 * num27);
                    NPC.velocity.Y = -(num26 * num27);
                    if (handSwipeTimer >= 45)
                    {
                        attackAI[2]++;
                        handSwipeTimer = 0;
                    }
                    NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 3.14f;
                    Vector2 direction = player.Center - NPC.Center;
                    if (direction.X > 0f)
                    {
                        NPC.spriteDirection = 1;
                    }
                    if (direction.X < 0f)
                    {
                        NPC.spriteDirection = -1;
                    }
                }
                if (attackAI[2] == 2) // swipe
                {
                    handSwipeTimer++;

                    float speed = 15f;
                    float num25 = player.Center.X - NPC.Center.X;
                    float num26 = player.Center.Y - NPC.Center.Y;
                    float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26);
                    num27 = speed / num27;
                    NPC.velocity.X = num25 * num27;
                    NPC.velocity.Y = num26 * num27;
                    if (handSwipeTimer >= 45 || Vector2.Distance(player.Center, NPC.Center) < 20 || Vector2.Distance(parent.Center, NPC.Center) > 500)
                    {
                        attackAI[2] = 0;
                        handSwipeTimer = 0;
                    }
                    NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
                    Vector2 direction = player.Center - NPC.Center;
                    if (direction.X > 0f)
                    {
                        NPC.spriteDirection = 1;
                    }
                    if (direction.X < 0f)
                    {
                        NPC.spriteDirection = -1;
                    }
                }
                NPC.localAI[0] = 0;
            }
            else
            {
                if (NPC.localAI[0] == 0)
                {
                    spinTimer = NPC.ai[0] == 1 ? 180 : 0;
                    spinDetectDelay = 30;
                    spinDontProj = false;
                    NPC.localAI[0]++;
                }
                int distance = 300;
                double rad = spinTimer * (Math.PI / 180); // angle to radians
                float spinX = parent.Center.X - (int)(Math.Cos(rad) * distance) - NPC.width / 2;
                float spinY = parent.Center.Y - (int)(Math.Sin(rad) * distance) - NPC.height / 2;
                Vector2 target = new Vector2(spinX, spinY);


                if (spinAI == 0)
                {
                    spinOrigin = target;
                    Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                    toTarget.Normalize();
                    if (Vector2.Distance(target, NPC.Center) > 20)
                    {
                        NPC.velocity = toTarget * 16;
                    }
                    else
                    {
                        spinAI = 1;
                    }
                    NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
                }
                else
                {
                    spinTimer += 3; // speed

                    NPC.velocity *= 0f;

                    NPC.position.X = spinX;
                    NPC.position.Y = spinY;

                    Vector2 direction = parent.Center - NPC.Center;
                    NPC.rotation = direction.ToRotation() + 1.57f;

                    spinProjTimer--;
                    if (spinProjTimer <= 0 && !spinDontProj)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<CrystalFlower>(), NPC.damage, 8f, 0)];
                        proj.rotation = NPC.rotation;
                        spinProjTimer = 3;
                    }
                }
                spinDetectDelay--;
                if (spinDetectDelay <= 0)
                {
                    if (Vector2.Distance(spinOrigin, NPC.Center) < 75)
                    {
                        spinDontProj = true;
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}