using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Aqueous
{
    public class AquaticReaper : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.noGravity = true;
            NPC.width = 24;
            NPC.height = 24;
            NPC.damage = 100;
            NPC.defense = 100;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 111, hit.HitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
        public override void AI()
        {
            NPC.noTileCollide = true;
            int num1029 = 90;
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(false);
                NPC.direction = 1;
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] == 0f)
            {
                float[] var_9_32483_cp_0 = NPC.ai;
                int var_9_32483_cp_1 = 1;
                float num244 = var_9_32483_cp_0[var_9_32483_cp_1];
                var_9_32483_cp_0[var_9_32483_cp_1] = num244 + 1f;
                int arg_324A9_0 = NPC.type;
                NPC.noGravity = true;
                NPC.dontTakeDamage = true;
                NPC.velocity.Y = NPC.ai[3];
                if (NPC.ai[1] >= (float)num1029)
                {
                    NPC.ai[0] = 1f;
                    NPC.ai[1] = 0f;
                    if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[1] = 1f;
                    }
                    SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.Center);
                    NPC.TargetClosest(true);
                    NPC.spriteDirection = NPC.direction;
                    Vector2 vector127 = Main.player[NPC.target].Center - NPC.Center;
                    float speed = 40f;
                    vector127.Normalize();
                    NPC.velocity = vector127 * speed;
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.direction == -1)
                    {
                        NPC.rotation += 3.14159274f;
                    }
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                NPC.noGravity = true;
                if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    if (NPC.ai[1] < 1f)
                    {
                        NPC.ai[1] = 1f;
                    }
                }
                else
                {
                    NPC.alpha -= 15;
                    if (NPC.alpha < 150)
                    {
                        NPC.alpha = 150;
                    }
                }
                if (NPC.ai[1] >= 1f)
                {
                    NPC.alpha -= 60;
                    if (NPC.alpha < 0)
                    {
                        NPC.alpha = 0;
                    }
                    NPC.dontTakeDamage = false;
                    float[] var_9_32858_cp_0 = NPC.ai;
                    int var_9_32858_cp_1 = 1;
                    float num244 = var_9_32858_cp_0[var_9_32858_cp_1];
                    var_9_32858_cp_0[var_9_32858_cp_1] = num244 + 1f;
                    if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        if (NPC.DeathSound != null)
                        {
                            SoundEngine.PlaySound(NPC.DeathSound, NPC.position);
                        }
                        NPC.life = 0;
                        NPC.HitEffect(0, 10.0);
                        NPC.active = false;
                        return;
                    }
                }
                if (NPC.ai[1] >= 60f)
                {
                    NPC.noGravity = false;
                }
                NPC.rotation = NPC.velocity.ToRotation();
                if (NPC.direction == -1)
                {
                    NPC.rotation += 3.14159274f;
                    return;
                }
            }
        }
    }
}