using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Infernace
{
    public class HealingHearth : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 26;
            NPC.lifeMax = 1000;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPCID.Sets.NeedsExpertScaling[NPC.type] = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath58;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 2000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 4000;
            }
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.1f;
            NPC parent = Main.npc[(int)NPC.ai[1]];
            NPC.localAI[1]++;
            if (!parent.active) NPC.active = false;
            else
            {
                if (parent.life < parent.lifeMax * 0.75f && Vector2.Distance(NPC.Center, parent.Center) < 600 && NPC.localAI[1] % 2 == 0) parent.life++;
            }
            Vector2 target = new Vector2(parent.Center.X, parent.Center.Y - 300);
            if (NPC.Center.X > parent.Center.X) target.X = parent.Center.X + 200;
            else target.X = parent.Center.X - 100;
            Move(target, 0.3f);
            if (NPC.localAI[0] == 0)
            {
                int numRocks = 3;
                if (Main.expertMode) numRocks = 5;
                if (MyWorld.awakenedMode) numRocks = 8;
                for (int i = 0; i < numRocks; i++)
                {
                    int type = ModContent.NPCType<RockSmall>();
                    int choice = Main.rand.Next(3);
                    if (choice == 0) type = ModContent.NPCType<RockSmall>();
                    else if (choice == 1) type = ModContent.NPCType<RockMedium>();
                    else if (choice == 2) type = ModContent.NPCType<RockLarge>();

                    NPC rock = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, type)];
                    rock.ai[1] = NPC.whoAmI;
                    rock.velocity = new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
                }
                NPC.localAI[0]++;
            }

            Lighting.AddLight(NPC.Center, (255 - NPC.alpha) * 0.4f / 255f, (255 - NPC.alpha) * 0.1f / 255f, (255 - NPC.alpha) * 0f / 255f);
            int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }
        private void Move(Vector2 target, float speed)
        {
            int maxDist = 500;
            if (Vector2.Distance(target, NPC.Center) >= maxDist)
            {
                float moveSpeed = 14f;
                Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                toTarget.Normalize();
                NPC.velocity = toTarget * moveSpeed;
            }
            else
            {
                NPC.spriteDirection = NPC.direction;

                float gotoX = target.X - NPC.Center.X;
                float gotoY = target.Y - NPC.Center.Y;
                if (NPC.velocity.X < gotoX)
                {
                    NPC.velocity.X = NPC.velocity.X + speed;
                    if (Vector2.Distance(target, NPC.Center) >= maxDist / 2)
                    {
                        NPC.velocity.X = NPC.velocity.X + speed * 2;
                    }
                }
                else if (NPC.velocity.X > gotoX)
                {
                    NPC.velocity.X = NPC.velocity.X - speed;
                    if (Vector2.Distance(target, NPC.Center) >= maxDist / 2)
                    {
                        NPC.velocity.X = NPC.velocity.X - speed * 2;
                    }
                }
                if (NPC.velocity.Y < gotoY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed;
                    if (Vector2.Distance(target, NPC.Center) >= maxDist / 2)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + speed;
                    }
                    if (NPC.velocity.Y < 0f && gotoY > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + speed;
                        return;
                    }
                }
                else if (NPC.velocity.Y > gotoY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed;
                    if (Vector2.Distance(target, NPC.Center) >= maxDist / 2)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - speed;
                    }
                    if (NPC.velocity.Y > 0f && gotoY < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - speed;
                        return;
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