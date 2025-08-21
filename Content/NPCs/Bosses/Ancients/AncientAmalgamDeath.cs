using ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Gores;
using ElementsAwoken.Content.Projectiles.Other;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Ancients
{
    public class AncientAmalgamDeath : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "ElementsAwoken/Content/NPCs/Bosses/Ancients/AncientAmalgam";
            }
        }
        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 102;
            NPC.aiStyle = -1;
            NPC.lifeMax = 10;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.scale *= 1.3f;
            NPC.npcSlots = 1f;
            Music = MusicID.LunarBoss;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // 0.n == уменшает изоброжения
                Scale = 0.8f, // Мини иконка в бестиарии 
                PortraitScale = 0.8f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0;
            value.Position.Y -= 15;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement(""),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            ]);
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
            if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
            {
                NPC.frame.Y = 0;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreKill()
        {
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ElementsAwoken.AADeathBall;
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            EAU.Sb.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), null, Color.White, NPC.rotation, origin, NPC.ai[0] / 450f, SpriteEffects.None, 0.0f);
        }

        public override void AI()
        {
            Player P = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, 1f, 1f, 1f);

            NPC.ai[0]++;

            float intensity = MathHelper.Lerp(0f, 1f, NPC.ai[0] / 450f);
            MoonlordDeathDrama.RequestLight(intensity, NPC.Center);
            if (NPC.ai[1] == 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<Lightbeam>(), 0, 0f, 0, i);
                }
                NPC.ai[1]++;
            }
            if (NPC.ai[0] > 450)
            {
                NPC.immortal = false;
                NPC.dontTakeDamage = false;
                NPC.SimpleStrikeNPC(NPC.life, 0, false, 0f, DamageClass.Default, false, 0, false);
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<DeathShockwave>(), 0, 0f);
                if (Main.netMode == 0 && !MyWorld.downedAncients) Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<CreditsStarter>(), 0, 0f);
                MyWorld.downedAncients = true;
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
    }
}
