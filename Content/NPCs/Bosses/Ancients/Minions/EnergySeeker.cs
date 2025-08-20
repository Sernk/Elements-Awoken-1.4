using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Ancients.Minions
{
    public class EnergySeeker : ModNPC
    {
        public float vectorX = 0f;
        public float vectorY = 0f;
        public int changeLocationTimer = 0;
        float speed = 0.1f;
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 40;
            NPC.damage = 40;
            NPC.defense = 30;
            NPC.lifeMax = 4000;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.Item27;
            NPC.buffImmune[24] = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 75;
            NPC.lifeMax = 7000;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];

            for (int i = 0; i < 2; i++)
            {
                int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AncientPink>());
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.1f;
            }

            Vector2 direction = P.Center - NPC.Center;
            if (direction.X > 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = direction.ToRotation();
            }
            if (direction.X < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = direction.ToRotation() - 3.14f;
            }
            NPC.ai[1]++;
            if (NPC.ai[1] < 400)
            {
                if (NPC.ai[2] == 0)
                {
                    int minDist = 150;
                    vectorX = Main.rand.Next(-500, 500);
                    vectorY = Main.rand.Next(-500, 500);
                    if (vectorX < minDist && vectorX > 0)
                    {
                        vectorX = minDist;
                    }
                    else if (vectorX > -minDist && vectorX < 0)
                    {
                        vectorX = -minDist;
                    }
                    if (vectorY < minDist && vectorY > 0)
                    {
                        vectorY = minDist;
                    }
                    else if (vectorY > -minDist && vectorX < 0)
                    {
                        vectorY = -minDist;
                    }
                    NPC.ai[2]++;
                    speed = Main.rand.NextFloat(0.3f, 0.4f);
                }
                // movement
                NPC.TargetClosest(true);
                NPC.spriteDirection = NPC.direction;
                Vector2 vector75 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float targetX = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) + vectorX - vector75.X + Main.rand.Next(-25, 25);
                float targetY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) + vectorY - vector75.Y + Main.rand.Next(-25, 25);
                if (NPC.velocity.X < targetX)
                {
                    NPC.velocity.X = NPC.velocity.X + speed * 2;
                }
                else if (NPC.velocity.X > targetX)
                {
                    NPC.velocity.X = NPC.velocity.X - speed * 2;
                }
                if (NPC.velocity.Y < targetY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed;
                    if (NPC.velocity.Y < 0f && targetY > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + speed;
                        return;
                    }
                }
                else if (NPC.velocity.Y > targetY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed;
                    if (NPC.velocity.Y > 0f && targetY < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - speed;
                        return;
                    }
                }
            }
            else
            {
                // dash
                NPC.ai[0]++;

                float speed = 10f;
                float num25 = P.Center.X - NPC.Center.X;
                float num26 = P.Center.Y - NPC.Center.Y;
                float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26);
                num27 = speed / num27;
                NPC.velocity.X = num25 * num27;
                NPC.velocity.Y = num26 * num27;
                if (NPC.ai[0] >= 40)
                {
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                }
                if (Vector2.Distance(P.Center, NPC.Center) < 30)
                {
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                }
            }
            if (Main.player[NPC.target].dead || !NPC.AnyNPCs(ModContent.NPCType<Izaris>()))
            {
                NPC.active = false;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            //gore
            if (NPC.life <= 0)
            {
                Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, Mod.Find<ModGore>("EnergySeeker").Type, NPC.scale);
            }
        }
    }
}