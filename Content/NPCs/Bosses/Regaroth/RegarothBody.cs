using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.NPCs.Bosses.Regaroth.RegarothMinion;
using ElementsAwoken.Content.Projectiles.NPCProj.RegarothP;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Regaroth
{
    public class RegarothBody : ModNPC
    {
        public int projectileBaseDamage = 50;
        public override void SetDefaults()
        {
            NPC.width = 52;
            NPC.height = 88;
            NPC.damage = 35;
            NPC.defense = 35;
            NPC.lifeMax = 100000;
            NPC.knockBackResist = 0.0f;
            NPC.scale = 1.1f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void FindFrame(int frameHeight)
        {
            if (Main.npc[(int)NPC.ai[3]].life < Main.npc[(int)NPC.ai[3]].lifeMax / 2)
            {
                NPC.frame.Y = frameHeight * 1;
            }
            else
            {
                NPC.frame.Y = 0;
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 45;
            if (MyWorld.awakenedMode)
            {
                NPC.damage = 60;
                NPC.defense = 45;
            }
        }
        public override bool PreAI()
        {
            bool expertMode = Main.expertMode;
            Player P = Main.player[NPC.target];
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead)
                NPC.timeLeft = 50;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NetMessage.SendData(28, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            // shoot code
            if (NPC.localAI[0] == 0)
            {
                NPC.localAI[0]++;
            }
            NPC.ai[2]++;
            NPC.ai[0]--;
            if (NPC.ai[2] > 1150f)
            {
                NPC.ai[2] = 0f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                float rotation = (float)Math.Atan2(NPC.Center.Y - (P.position.Y + (P.height * 0.5f)), NPC.Center.X - (P.position.X + (P.width * 0.5f)));
                if (NPC.ai[2] >= 750)
                {
                    if (Main.rand.Next(250) == 0)
                    {
                        float Speed = 10f;
                        SoundEngine.PlaySound(SoundID.Item12, NPC.position);
                        int num54 = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<RegarothBolt>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                }
                if (NPC.ai[2] <= 750)
                {
                    if (Main.rand.Next(4000) == 0)
                    {
                        float Speed = 4f;
                        SoundEngine.PlaySound(SoundID.Item12, NPC.position);
                        int num54 = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<RegarothPortal>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                }
                if (Main.rand.Next(1500) == 0)
                {
                    float Speed = 4f;
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                    int num54 = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<RegarothBomb>(), projectileBaseDamage + 20, 0f, Main.myPlayer);
                }
                if (NPC.ai[0] <= 0)
                {
                    if (Main.rand.Next(20) == 0)
                    {
                        Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
                        NPC.NewNPC(EAU.NPCs(NPC), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<RegarothMinionHead>());
                    }
                    NPC.ai[0] = 500f;
                }
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, Mod.Find<ModGore>("RegarothBody").Type, 1.1f);
                NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
                NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
                NPC.width = 50;
                NPC.height = 50;
                NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
                NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
            }
            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - (float)NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                NPC.velocity = Vector2.Zero;
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;

            }
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;       //this make that the npc does not have a health bar
        }
    }
}
