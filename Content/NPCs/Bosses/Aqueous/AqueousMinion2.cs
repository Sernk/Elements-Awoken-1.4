using ElementsAwoken.Content.Projectiles.NPCProj.Aqueous;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Aqueous
{
    public class AqueousMinion2 : ModNPC
    {
        public float shootTimer = 180f;

        public override void SetDefaults()
        {
            NPC.npcSlots = 0f;
            NPC.damage = 19;
            NPC.width = 26; //324
            NPC.height = 20; //216
            NPC.defense = 30;
            NPC.lifeMax = 5000;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.buffImmune[24] = true;
            NPC.noTileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aqueous Minion");
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 1f, 1f, 1f);
            //npc.position.X = Main.player[npc.target].position.X + -350;
            //npc.position.Y = Main.player[npc.target].position.Y + -350;
            NPC.TargetClosest(true);
            if (NPC.velocity.X >= 0f)
            {
                NPC.spriteDirection = 1;
                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                NPC.rotation = direction.ToRotation();
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                NPC.rotation = direction.ToRotation() - 3.14f;
            }
            Player P = Main.player[NPC.target];

            if (!P.active)
            {
                NPC.active = false;
            }
            Vector2 offset = new Vector2(500, 0);
            NPC.ai[0]-= 0.01f;
            NPC.Center = P.Center + offset.RotatedBy(NPC.ai[0] + NPC.ai[1] * (Math.PI * 2 / 8));

            if (shootTimer > 0f)
            {
                shootTimer -= 1f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer == 0f)
            {
                float Speed = 3.5f;  //projectile speed
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                int damage = 30;  //projectile damage
                int type = ModContent.ProjectileType<Waterball>();  //put your projectile
                SoundEngine.PlaySound(SoundID.Item21, NPC.position);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(EAU.NPCs(NPC), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                shootTimer = 150f + Main.rand.Next(-30, 50);
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<Aqueous>()))
            {
                NPC.active = false;
            }
        }
    }
}