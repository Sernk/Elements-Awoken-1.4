using ElementsAwoken.Content.Projectiles.NPCProj.ScourgeFighter;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.ScourgeFighter
{
    public class MiniFighter : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.aiStyle = 5;
            NPC.damage = 30;
            NPC.width = 30; //324
            NPC.height = 26; //216
            NPC.defense = 30;
            NPC.lifeMax = 750;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement(""),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
            ]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)EAU.BalanceHP(500, balance, bossAdjustment, 1000, roundTo: 100);
            NPC.damage = (int)EAU.BalanceDamage(30, numPlayers, balance, 60);
            NPC.defense = EAU.BalanceDefense(30, 45);
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            NPC.ai[0]++;
            if (NPC.ai[0] == 70)
            {
                float Speed = 20f;  //projectile speed
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                int damage = 20;  //projectile damage
                int type = ModContent.ProjectileType<ScourgeBeam>();  //put your projectile
                SoundEngine.PlaySound(SoundID.Item33, NPC.position);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(EAU.NPCs(NPC), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                NPC.ai[0] = 0;
            }
            for (int num246 = 0; num246 < 2; num246++)
            {
                float num247 = 0f;
                float num248 = 0f;
                if (num246 == 1)
                {
                    num247 = NPC.velocity.X * 0.5f;
                    num248 = NPC.velocity.Y * 0.5f;
                }
            }
            for (int k = 0; k < 200; k++)
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
            if (!NPC.AnyNPCs(ModContent.NPCType<ScourgeFighter>()))
            {
                NPC.active = false;
            }
        }
    }
}